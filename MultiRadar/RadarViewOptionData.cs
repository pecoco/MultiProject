using MultiRadar.Structure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ACT.RadarViewOrder.RadarViewOrder;

namespace MultiRadar
{
    public class RadarViewOptionData
    {
        private RadarViewType radarViewOption;
        string fileName;
        ViewOption voMob ;
        ViewOption voHum ;
        ViewOption voId ;
        public bool IsNameView(RadarZoomSelect mode)
        {
            switch (mode)
            {
                case RadarZoomSelect.mob:
                    return voMob.name;
                case RadarZoomSelect.hum:
                    return voHum.name;
                case RadarZoomSelect.id:
                    return voId.name;
                default:
                    return true;
            }
        }
        public bool IsPositionView(RadarZoomSelect mode)
        {
            switch (mode)
            {
                case RadarZoomSelect.mob:
                    return voMob.positon;
                case RadarZoomSelect.hum:
                    return voHum.positon;
                case RadarZoomSelect.id:
                    return voId.positon;
                default:
                    return false;
            }
        }

        public bool IsHpView(RadarZoomSelect mode)
        {
            switch (mode)
            {
                case RadarZoomSelect.mob:
                    return voMob.hp;
                case RadarZoomSelect.hum:
                    return voHum.hp;
                case RadarZoomSelect.id:
                    return voId.hp;
                default:
                    return false;
            }
        }

        public bool IsJobView(RadarZoomSelect mode)
        {
            switch (mode)
            {
                case RadarZoomSelect.mob:
                    return voMob.job;
                case RadarZoomSelect.hum:
                    return voHum.job;
                case RadarZoomSelect.id:
                    return voId.job;
                default:
                    return false;
            }
        }

        public bool IsLinkView(RadarZoomSelect mode)
        {
            switch (mode)
            {
                case RadarZoomSelect.mob:
                    return voMob.link;
                case RadarZoomSelect.hum:
                    return voHum.link;
                case RadarZoomSelect.id:
                    return voId.link;
                default:
                    return false;
            }
        }


        public RadarViewOptionData(string fileNamePath)
        {
            this.fileName = fileNamePath;
            if (File.Exists(this.fileName))
            {
                LoadAreaData(fileName);
                voMob = radarViewOption.mob;
                voHum = radarViewOption.hum;
                voId = radarViewOption.id;
            }
            else
            {
                radarViewOption = new RadarViewType();
                SaveAreaData(fileName);//空のデーターを作成して保存
            }
        }
        //保存
        public void SaveAreaData(string fileNamePath = "")
        {
            if (fileNamePath.Length > 0)
            {
                fileName = fileNamePath;
            }

            try
            {
                System.Xml.Serialization.XmlSerializer serializer =
                new System.Xml.Serialization.XmlSerializer(typeof(RadarViewType));
                System.IO.StreamWriter sw = new System.IO.StreamWriter(
                fileName, false, new System.Text.UTF8Encoding(false));
                //シリアル化し、XMLファイルに保存する
                serializer.Serialize(sw, radarViewOption);
                sw.Close();
            }
            catch (IOException e)
            {
                System.Windows.MessageBox.Show(e.Message + "(" + fileName + ")MultiProjectResourcesフォルダが存在しないか、RadarOption.xmlが存在しません", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);

            }
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
                new System.Xml.Serialization.XmlSerializer(typeof(RadarViewType));
            System.IO.StreamReader sr = new System.IO.StreamReader(
                fileName, new System.Text.UTF8Encoding(false));
            //XMLファイルから読み込み、逆シリアル化する
            radarViewOption = (RadarViewType)serializer.Deserialize(sr);
            sr.Close();

        }
    }
}
