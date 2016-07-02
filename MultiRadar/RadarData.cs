using System.Collections.Generic;
using System.IO;
namespace MultiRadar
{
    public class RadarData
    {
        Zones area;
        string fileName ="";
        public RadarData(string fileNamePath)
        {
            this.fileName = fileNamePath;
            if (File.Exists(this.fileName))
            {
                LoadAreaData(fileName);
            }
            else
            {
                area = new Zones();
                SaveAreaData(fileName);//空のデーターを作成して保存
            }
        }
        public List<string> ZoneList()
        {
            List<string> list = new List<string>();
            foreach (ZoneMobData zoneData in area.zones)
            {
                list.Add(zoneData.name);
            }
            return list;
        }
        public void AddZone(string zoneName, string zoneNameJp)
        {
            foreach (ZoneMobData zoneData in area.zones)
            {
                if (zoneData.name == zoneName)
                {
                    zoneData.nameJp = zoneNameJp;
                    return;//ＪＰだけ変更して離脱
                }
            }
            ZoneMobData zone = new ZoneMobData();
            zone.name = zoneName;
            zone.nameJp = zoneNameJp;
            area.zones.Add(zone);
        }

        //検索リストに追加
        public bool AddMob(string zoneName, MobType mobType, string name, string ZoneNameJp ="")
        {
            if (area != null)
            {
                bool addZone = false;
                bool addName = false;
                zoneName = zoneName.Trim();
                name = name.Trim();
                ZoneNameJp = ZoneNameJp.Trim();

                foreach (ZoneMobData zoneData in area.zones)
                {
                    if (zoneData.name == zoneName)
                    {

                        List<string> mobs = new List<string>();
                        switch (mobType)
                        {
                            case MobType.S:
                                mobs = zoneData.s; break;
                            case MobType.A:
                                mobs = zoneData.a; break;
                            case MobType.B:
                                mobs = zoneData.b; break;
                            case MobType.ETC:
                                mobs = zoneData.etc; break;

                        }
                        foreach (string mob in mobs)
                        {
                            if (mob == name)
                            {
                                //既に存在する
                                return false;
                            }
                        }
                        //エリア名は存在する＆モブ名前がない
                        addName = true;

                    }
                    if (addName == true)
                    {
                        switch (mobType)
                        {
                            case MobType.S:
                                zoneData.s.Add(name); break;
                            case MobType.A:
                                zoneData.a.Add(name); break;
                            case MobType.B:
                                zoneData.b.Add(name); break;
                            case MobType.ETC:
                                zoneData.etc.Add(name); break;
                        }
                        return true;
                    }
                }
                //ゾーンから新規登録
                if (addZone == false)
                {
                    ZoneMobData zone = new ZoneMobData();
                    zone.name = zoneName;
                    switch (mobType)
                    {
                        case MobType.S:
                            zone.s.Add(name); break;
                        case MobType.A:
                            zone.a.Add(name); break;
                        case MobType.B:
                            zone.b.Add(name); break;
                        case MobType.ETC:
                            zone.etc.Add(name); break;
                    }
                    area.zones.Add(zone);
                    return true;
                }
            }
            return false;
        }

        //リストから削除
        public void RemoveMob(string zoneName, MobType mobType, string name)
        {
            if (AddMob(zoneName, mobType, name) == false)
            {
                foreach (ZoneMobData zoneData in area.zones)
                {
                    if (zoneData.name == zoneName)
                    {
                        switch (mobType)
                        {
                            case MobType.S:
                                zoneData.s.Remove(name);  break;
                            case MobType.A:
                                zoneData.a.Remove(name); break;
                            case MobType.B:
                                zoneData.b.Remove(name); break;
                            case MobType.ETC:
                                zoneData.etc.Remove(name); break;
                        }
                        return;
                    }
                }
            }
        }

        //定義データーを取得する
        public ZoneMobData getMobList(string zoneName)
        {
            foreach (ZoneMobData zoneData in area.zones)
            {
                if (zoneData.name == zoneName)
                {
                    return zoneData;
                }
            }
            return null;
        }

        //保存
        public void SaveAreaData(string fileNamePath = "")
        {
            if (fileNamePath.Length>0)
            {
                fileName = fileNamePath;
            }
            
            System.Xml.Serialization.XmlSerializer serializer =
            new System.Xml.Serialization.XmlSerializer(typeof(Zones));
            System.IO.StreamWriter sw = new System.IO.StreamWriter(
            fileName, false, new System.Text.UTF8Encoding(false));
            //シリアル化し、XMLファイルに保存する
            serializer.Serialize(sw, area);
            sw.Close();
        }

        //読み出し
        public void LoadAreaData(string fileNamePath = "")
        {
            if (fileNamePath.Length > 0)
            {
                fileName = fileNamePath;
            }
            //XmlSerializerオブジェクトを作成
            System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(Zones));
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));
            //XMLファイルから読み込み、逆シリアル化する
            area = (Zones)serializer.Deserialize(sr);
            sr.Close();

        }

        //検索
        public int Search(string zoneName, string mobName)
        {
            if (zoneName == null)
            {
                return 0;
            }
            ZoneMobData rm = getMobList(zoneName);
            if (rm != null)
            {
                foreach (string mob in rm.s)
                {
                    if (mob == mobName) { return 1; }
                }
                foreach (string mob in rm.a)
                {
                    if (mob == mobName) { return 2; }
                }
                foreach (string mob in rm.b)
                {
                    if (mob == mobName) { return 3; }
                }
                foreach (string mob in rm.etc)
                {
                    if (mob == mobName) { return 4; }
                }
            }
            return 0;
        }
    }
}

