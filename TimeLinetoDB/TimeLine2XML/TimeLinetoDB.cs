using System;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

using System.Drawing;
using System.IO;
using System.Text;

using System.Windows.Forms;

//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Management.Common;

namespace TimeLine2XML
{
    public partial class TimeLinetoDB : Form
    {
        const int HEADER_ID = 0;
        const int HEADER_COMMENT = 1;
        const int HEADER_DEFINTION = 2;
        const int HEADER_OPTION = 3;
        const int HEADER_TITLE = 4;
        const int HEADER_DEVICE = 5;
        const int HEADER_SPEED = 6;
        const int HEADER_VOL = 7;

        const int HEADER_ALERTALL_ID = 0;
        const int HEADER_ALERTALL_COMMENT = 1;
        const int HEADER_ALERTALL_DEFINTION = 2;
        const int HEADER_ALERTALL_TITLE = 3;
        const int HEADER_ALERTALL_VOLUE1 = 4;
        const int HEADER_ALERTALL_VOLUE2 = 5;
        const int HEADER_ALERTALL_VOLUE3 = 6;
        const int HEADER_ALERTALL_ACTOR = 7;
        const int HEADER_ALERTALL_MES = 8;

        //Grid col Index 
        const int DATA_ID = 0;
        const int DATA_COMMENT = 1;
        const int DATA_SEC = 2;
        const int DATA_TITLE = 3;
        const int DATA_DURATION = 4;
        const int DATA_DURATION_SEC = 5;
        const int DATA_SYNC = 6;
        const int DATA_SYNC_TEXT = 7;
        const int DATA_WINDOW = 8;
        const int DATA_WINDOW_VALUE = 9;

        //TimeLine Text 
        const int TEXT_SEC = 0;
        const int TEXT_TITLE = 1;
        const int TEXT_COL1 = 2;
        const int TEXT_COL2 = 4;
        const int TEXT_COL3 = 6;


        public TimeLinetoDB()
        {
            InitializeComponent();
        }

        private bool ExceptionError = false; 
        private void ReadTextFile(string filename)
        {
            ExceptionError = false;
            dataGV.RowCount = 1;
            headerGV.RowCount = 1;
            alertallGV.RowCount = 1;
            speakers.Clear();
            //StringBuilder sb = new StringBuilder();
            //"C:\test\1.txt"をShift-JISコードとして開く

            System.IO.FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader sr = new StreamReader(
            fs,
            Encoding.GetEncoding("shift_jis"));
            try
            {
                long seekPos = 0;
                StringBuilder sbBuff = new StringBuilder();
                while (sr.Peek() >= 0 && ExceptionError == false)
                {
                    //sb.Length = 0;
                    sbBuff.Length = 0;
                    sbBuff.Append(sr.ReadLine());
                    if (!HeaderAnalyze(sbBuff.ToString()))
                    {
                        sr.Close();
                        fs.Close();//一回閉じないとシークが効かない
                        sr.Dispose();
                        fs.Dispose();

                        System.Threading.Thread.Sleep(100);
                        fs = new FileStream(filename, FileMode.Open);
                        sr = new StreamReader(fs, Encoding.GetEncoding("shift_jis"));
                        sr.BaseStream.Seek(seekPos, System.IO.SeekOrigin.Begin);
                        System.Threading.Thread.Sleep(100);
                        break;
                    }
                    seekPos += (LenB(sbBuff.ToString())+2);//バイト数カウント + CRLF(2byte) 
                }
                while (sr.Peek() >= 0 && ExceptionError == false)
                {
                    //sb.Length = 0;
                    Analyze(sr.ReadLine());
                }
            }
            catch (Exception e)
            {
                sr.Close();
                fs.Close();　
                sr.Dispose();
                fs.Dispose();
            }
            finally
            {
                sr.Close();
                fs.Close();
                sr.Dispose();
                fs.Dispose();
            }
            AutoIdCreate();
        }

        private static int LenB(string stTarget)
        {
            return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget);
        }


        private void SaveTextFile(string filename)
        {
            using (System.IO.StreamWriter wr = new System.IO.StreamWriter(
                filename, false,
                System.Text.Encoding.GetEncoding("shift_jis")))
            {
                bool continueLoop=true;
                int gridIndex = 0;

                while (continueLoop)
                {
                    wr.WriteLine(ExportHederText(ref continueLoop, ref gridIndex));
                }

                gridIndex = 0;
                continueLoop = true;
                while (continueLoop)
                {
                    wr.WriteLine(ExportalertallText(ref continueLoop, ref gridIndex));
                }

                gridIndex = 0;
                continueLoop = true;
                while (continueLoop)
                {
                    wr.WriteLine(ExportText( ref continueLoop , ref gridIndex));
                }

            }

        }


        private string ExportHederText(int gridRowIndex)
        {
            int row = gridRowIndex;
            bool continueLoop = true;
            return ExportHederText(ref continueLoop, ref row);

        }

        private string ExportalertallText(int gridRowIndex)
        {
            int row = gridRowIndex;
            bool continueLoop = true;
            return ExportalertallText(ref continueLoop, ref row);

        }

        private string ExportText(int gridRowIndex)
        {
            int row = gridRowIndex;
            bool continueLoop = true;
            return ExportText(ref continueLoop, ref row);
        }



        private string ExportHederText(ref bool continueLoop, ref int gridIndex)
        {
            if (gridIndex > headerGV.RowCount - 2)
            {
                continueLoop = false;
                return "";
            }
            StringBuilder sb = new StringBuilder();
            DataGridViewRow oneRow = headerGV.Rows[gridIndex];
            foreach (DataGridViewCell c in oneRow.Cells)
            {
                c.Value = c.Value ?? "";

                switch (c.ColumnIndex)
                {
                    case HEADER_ID: break;
                    case HEADER_COMMENT: sb.Append(c.Value.ToString()); break;
                    case HEADER_DEFINTION: sb.Append(c.Value.ToString()); break;
                    case HEADER_OPTION: sb.Append(c.Value.ToString()); break;
                    case HEADER_TITLE:
                        if (oneRow.Cells[DATA_COMMENT].Value.ToString() == "#" && c.Value.ToString()[0] == '#')
                        {
                            sb.Append('"' + c.Value.ToString().Substring(1) + '"');
                        }
                        else
                        {

                            sb.Append('"' + c.Value.ToString() + '"');
                        }
                        break;
                    case HEADER_DEVICE: sb.Append('"' + c.Value.ToString() + '"'); break;
                    case HEADER_SPEED: sb.Append(c.Value.ToString()); break;
                    case HEADER_VOL: sb.Append(c.Value.ToString()); break;


                }
                if (c.Value.ToString() != "" && c.ColumnIndex != HEADER_COMMENT && c.ColumnIndex != HEADER_ID) { sb.Append(' '); }
            }
            gridIndex++;
            return sb.ToString().Trim();
        }


        private string ExportalertallText(ref bool continueLoop, ref int gridIndex)
        {
            if (gridIndex > alertallGV.RowCount - 2)
            {
                continueLoop = false;
                return "";
            }
            StringBuilder sb = new StringBuilder();
            DataGridViewRow oneRow = alertallGV.Rows[gridIndex];
            foreach (DataGridViewCell c in oneRow.Cells)
            {
                c.Value = c.Value ?? "";

                switch (c.ColumnIndex)
                {
                    case HEADER_ALERTALL_ID: break;
                    //case HEADER_ALERTALL_COMMENT: sb.Append(c.Value.ToString()); break;
                    case HEADER_ALERTALL_DEFINTION: sb.Append(c.Value.ToString()); break;
                    case HEADER_ALERTALL_TITLE:
                        if (oneRow.Cells[DATA_COMMENT].Value.ToString() == "#" && c.Value.ToString()[0] == '#')
                        {
                            sb.Append('"' + c.Value.ToString().Substring(1) + '"');
                        }
                        else
                        {
                            sb.Append('"' + c.Value.ToString() + '"');
                        }
                        break;
                    case HEADER_ALERTALL_VOLUE1: sb.Append(c.Value.ToString()); break;
                    case HEADER_ALERTALL_VOLUE2: sb.Append(c.Value.ToString()); break;
                    case HEADER_ALERTALL_VOLUE3: sb.Append(c.Value.ToString()); break;
                    case HEADER_ALERTALL_ACTOR: sb.Append('"' + c.Value.ToString() + '"'); break;
                    case HEADER_ALERTALL_MES: sb.Append('"' + c.Value.ToString() + '"'); break;

                }
                if (c.Value.ToString() != "" && c.ColumnIndex != HEADER_ALERTALL_COMMENT && c.ColumnIndex != HEADER_ALERTALL_ID) { sb.Append(' '); }
            }
            gridIndex++;
            return sb.ToString().Trim();
        }



        private string ExportText(ref bool continueLoop, ref int gridIndex)
        {
            if(gridIndex> dataGV.RowCount - 2)
            {
                continueLoop = false;
                return "";
            }
            StringBuilder sb = new StringBuilder();
            DataGridViewRow oneRow = dataGV.Rows[gridIndex];
            foreach ( DataGridViewCell c in oneRow.Cells)
            {
                c.Value = c.Value ?? "";

                switch (c.ColumnIndex)
                {
                    case DATA_ID: break;
                    //case COMMENT: sb.Append(c.Value.ToString()); break;
                    case DATA_SEC: sb.Append(c.Value.ToString()); break;
                    case DATA_TITLE:
                        if (oneRow.Cells[DATA_COMMENT].Value.ToString() == "#" && c.Value.ToString()[0] == '#')
                        {
                            sb.Append('"' + c.Value.ToString().Substring(1) + '"');
                        }
                        else
                        {
                            sb.Append('"' + c.Value.ToString() + '"');
                        }
                        break;
                    case DATA_DURATION: sb.Append(c.Value.ToString()); break;
                    case DATA_DURATION_SEC: sb.Append(c.Value.ToString()); break;
                    case DATA_SYNC: sb.Append(c.Value.ToString()); break;
                    case DATA_SYNC_TEXT:
                        if (oneRow.Cells[DATA_SYNC].Value.ToString() != "")
                        {
                            sb.Append('/' + c.Value.ToString() + "/");
                        }
                        break;
                    case DATA_WINDOW: sb.Append(c.Value.ToString()); break;
                    case DATA_WINDOW_VALUE:
                        if (oneRow.Cells[DATA_SYNC].Value.ToString() != "")
                        {
                            sb.Append(c.Value.ToString());
                        }
                        break;
                }
                if (c.Value.ToString() != "" && c.ColumnIndex!= DATA_COMMENT && c.ColumnIndex != DATA_ID) { sb.Append(' '); }
            }
            gridIndex++;
            return sb.ToString().Trim();
        }


        private void AutoIdCreate()
        {
            for(int i = 0; i<dataGV.RowCount-1;i++)
            {
                dataGV[DATA_ID, i].Value = (i + 1);
            }
            for (int i = 0; i < headerGV.RowCount - 1; i++)
            {
                headerGV[DATA_ID, i].Value = (i + 1);
            }

            for (int i = 0; i < alertallGV.RowCount - 1; i++)
            {
                alertallGV[DATA_ID, i].Value = (i + 1);
            }
        }

        const int HEADER_TEXT_SEC = 0;
        const int HEADER_TEXT_DEFINTION = 2;

        List<string> speakers = new List<string>();
        private bool HeaderAnalyze(string s, int editRow = -1)
        {
            if (s == "") { return true; }

            string[] linebuf = s.Split(' ');

            if (linebuf.Length == 0) { return true; }

            if (linebuf[0].IndexOf("#") != -1)
            {

                linebuf[0] = linebuf[0].Trim().Substring(1);
                int row;
                //コメント文
                if (editRow == -1)
                {
                    headerGV.Rows.Add();
                    row = headerGV.RowCount - 2;
                    headerGV[HEADER_COMMENT, row].Value = "#";
                    headerGV[HEADER_TITLE, row].Value = s;
                }
                else
                {
                    row = editRow;
                    if(linebuf[0] == "define")
                    {
                        headerGV[HEADER_DEVICE, row].Value = "";
                        headerGV[HEADER_SPEED, row].Value = "";
                        headerGV[HEADER_VOL, row].Value = "";
                        headerGV[HEADER_DEFINTION, row].Value = "";
                        headerGV[HEADER_TITLE, row].Value = s;
                    }
                    if (linebuf[0] == "alertall")
                    {
                        alertallGV[HEADER_ALERTALL_VOLUE1, row].Value = "";
                        alertallGV[HEADER_ALERTALL_VOLUE2, row].Value = "";
                        alertallGV[HEADER_ALERTALL_VOLUE3, row].Value = "";
                        alertallGV[HEADER_ALERTALL_ACTOR, row].Value = "";
                        alertallGV[HEADER_ALERTALL_MES, row].Value = "";
                        alertallGV[HEADER_ALERTALL_DEFINTION, row].Value = "";
                        alertallGV[HEADER_ALERTALL_TITLE, row].Value = s;
                    }
                }
                return true;
            }
            string[] line = linebuf;
            if (line.Length == 0) { return true; }

            sb.Length = 0;
            sb.Append(line[HEADER_TEXT_SEC].Trim().ToLower());

            int chkNo;
            if(int.TryParse(sb.ToString(),out chkNo))
            {
                return false;
            }


            
            try
            {
                int row;


                if(sb.ToString()== "define")
                {
                    if (editRow == -1)
                    {
                        headerGV.Rows.Add();
                        row = headerGV.RowCount - 2;
                    }
                    else
                    {
                        row = editRow;
                    }

                    //define speaker "Fast" 3 100
                    headerGV[HEADER_DEFINTION, row].Value = "define";
                    headerGV[HEADER_OPTION, row].Value = "speaker";
                    headerGV[HEADER_TITLE, row].Value = line[2].Trim(new char[] { '"' });
                    speakers.Add(headerGV[HEADER_TITLE, row].Value.ToString());

                    int nextInt = 0;
                    string sText = line[3].Trim();

                    int checkInt;

                    if(int.TryParse(line[3],out checkInt)){
                        headerGV[HEADER_DEVICE, row].Value = "";
                        headerGV[HEADER_SPEED, row].Value = line[3];
                        headerGV[HEADER_VOL, row].Value = line[4];
                    }
                    else{

                        if (sText[0] == '"' && sText[sText.Length - 1] == '"')
                        {
                            //スペースがない
                            headerGV[HEADER_DEVICE, row].Value = line[3].Trim(new char[] { '"' });
                        }
                        else
                        {
                            sb.Length = 0;
                            sb.Append(line[3]);
                            while (true)
                            {
                                if (sb.ToString()[0] == '"' && sb.ToString()[sb.Length - 1] == '"')
                                {
                                    break;
                                }
                                sb.Append(' ' + line[4 + nextInt]);
                                nextInt++;
                                if(nextInt+4>= line.Length)
                                {
                                    //Err
                                    return true;
                                }
                            }
                            headerGV[HEADER_DEVICE, row].Value = sb.ToString().Trim(new char[] { '"' });
                            headerGV[HEADER_SPEED, row].Value = line[4+ nextInt];
                            headerGV[HEADER_VOL, row].Value = line[4+ nextInt+1];
                        }
                    }
                    //headerGV[HEADER_VOLUE1, row].Value = line[3].Trim();
                    //headerGV[HEADER_VOLUE2, row].Value = line[4].Trim();

                    

                }
                else if(sb.ToString() == "alertall")
                {
                    if (editRow == -1)
                    {
                        alertallGV.Rows.Add();
                        row = alertallGV.RowCount - 2;
                    }
                    else
                    {
                        row = editRow;
                    }


                    DataGridViewComboBoxCell cbc =
                    (DataGridViewComboBoxCell)alertallGV.Rows[row].Cells[HEADER_ALERTALL_ACTOR];
                    foreach (string itemName in speakers)
                    {
                        cbc.Items.Add(itemName);
                    }



                    alertallGV[HEADER_ALERTALL_DEFINTION, row].Value = "alertall";
                    alertallGV[HEADER_ALERTALL_TITLE, row].Value = line[1]; ;
                    alertallGV[HEADER_ALERTALL_VOLUE1, row].Value = "before";
                    alertallGV[HEADER_ALERTALL_VOLUE2, row].Value = line[3].Trim();
                    alertallGV[HEADER_ALERTALL_VOLUE3, row].Value = line[4].Trim();
                    alertallGV[HEADER_ALERTALL_ACTOR, row].Value = line[5].Trim(new char[] { '"' });

                    int nextInt=0;
                    sb.Length = 0;
                    sb.Append(line[6]);
                    while (true)
                    {
                        if (sb.ToString()[0] == '"' && sb.ToString()[sb.Length - 1] == '"')
                        {
                            break;
                        }
                        nextInt++;
                        if (nextInt + 6 > line.Length)
                        {
                            //Err
                            return true;
                        }
                        sb.Append(' ' + line[6 + nextInt]);
                    }
                    alertallGV[HEADER_ALERTALL_MES, row].Value = sb.ToString().Trim(new char[] { '"' });

                }
                else
                {
                    //未知の定義
                    MessageBox.Show("コメントのみ可能な文字列です.","エラー");
                    return false;
                }
                //headerGV[HEADER_DEFINTION, row].Value = line[HEADER_TEXT_DEFINTION].Trim();
                //headerGV[TITLE, row].Value = line[TEXT_TITLE].Trim(new char[] { '"' });

                //alertallGV
            }
            catch (Exception e)
            {
                int row = alertallGV.RowCount - 2;
                MessageBox.Show(e.ToString(), "Error:(" + row.ToString() + ")", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExceptionError = true;
            }
            return true;

        }


        private StringBuilder sb = new StringBuilder();
        System.Collections.Generic.List<string> lineWork = new List<string>();
        int[] inTextCols = new int[] { TEXT_COL1, TEXT_COL2, TEXT_COL3 };

        private void Analyze(string s, int editRow = -1)
        {


            if (s == "") { return; }
            string[] linebuf = s.Split(' ');
            if(linebuf.Length == 0) { return; }

            if (linebuf[0].IndexOf("#") != -1)
            {
                int row;
                //コメント文
                if (editRow == -1)
                {
                    dataGV.Rows.Add();
                    row = dataGV.RowCount - 2;
                    dataGV[DATA_COMMENT, row].Value = "#";
                }
                else{
                    toolStripStatusLb.Text = s;    
                    row = editRow;
                    dataGV[DATA_SYNC_TEXT, row].Value = "";
                    dataGV[DATA_DURATION_SEC, row].Value = "";
                    dataGV[DATA_WINDOW_VALUE, row].Value = "";
                }
                dataGV[DATA_DURATION, row].Value = "";
                dataGV[DATA_SYNC, row].Value = "";
                dataGV[DATA_WINDOW, row].Value = "";
                dataGV[DATA_TITLE, row].Value = s;
                return;
            }

            lineWork = new List<string>();
            bool isSync = false;
            sb.Length = 0;
            foreach (string str in linebuf)
            {
                if (isSync) {
                    if ((str.TrimEnd())[str.Length-1] == '/')
                    {
                        sb.Append(str);
                        lineWork.Add(sb.ToString().Replace("/",""));
                        isSync = false;
                    }else{
                        sb.Append(str);
                    }
                }
                else{
                    if (str.ToLower().IndexOf("sync") == -1)
                    {
                        lineWork.Add(str);
                    }
                    else{
                        lineWork.Add(str);
                        isSync = true;
                        sb.Length = 0;
                    }
                }
            }

            string[] line = lineWork.ToArray();
            if (line.Length == 0) { return; }
            try
            {
                int row;
                if (editRow == -1)
                {
                    dataGV.Rows.Add();
                    row = dataGV.RowCount - 2;
                }else
                {
                    row = editRow;
                }
                dataGV[DATA_SEC, row].Value = line[TEXT_SEC].Trim();
                dataGV[DATA_TITLE, row].Value = line[TEXT_TITLE].Trim(new char[] { '"' });

                dataGV[DATA_DURATION, row].Value = "";
                dataGV[DATA_SYNC, row].Value = "";
                dataGV[DATA_WINDOW, row].Value = "";

                if (line.Length > TEXT_COL1)
                {
                    int col = 0;
                    if (!SetGriddata(DATA_DURATION, DATA_DURATION_SEC, "duration", ref col, line, row)){return;}
                    if (!SetGriddata(DATA_SYNC, DATA_SYNC_TEXT, "sync", ref col, line, row)){return;}
                    if (!SetGriddata(DATA_WINDOW, DATA_WINDOW_VALUE, "window", ref col, line, row)){return;}
                }
            }
            catch (Exception e)
            {
                int row = dataGV.RowCount - 2;
                MessageBox.Show(e.ToString(), "Error:("+ row.ToString()+")", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExceptionError = true;
            }
        }
        private bool SetGriddata(int gridIndex , int gridIndexValue, string matchText ,ref int  col ,string[] line ,int row)
        {
            if (inTextCols[col] < line.Length)
            {
                if ( line[inTextCols[col]].Trim().ToLower() == matchText)
                {
                    dataGV[gridIndex, row].Value = matchText;
                    dataGV[gridIndexValue, row].Value = line[inTextCols[col] + 1].Trim();
                    col++;
                }
                return true;
            }else
            {
                return false;
            }
        }


        //Chenge Comment ComboBox
        private DataGridViewComboBoxEditingControl headerGV_dataGridViewComboBox = null;
        private DataGridViewComboBoxEditingControl alertallGV_dataGridViewComboBox = null;
        private DataGridViewComboBoxEditingControl dataGV_dataGridViewComboBox = null;

        private void headerGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                if (e.Control is DataGridViewComboBoxEditingControl)
                {
                    DataGridView dgv = (DataGridView)sender;
                    if (dgv.Name == "headerGV")
                    {
                        if (dgv.CurrentCell.OwningColumn.Name == "HeaderCommentOut")
                        {
                            this.headerGV_dataGridViewComboBox =
                                (DataGridViewComboBoxEditingControl)e.Control;
                            this.headerGV_dataGridViewComboBox.SelectedIndexChanged +=
                                new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
                        }
                    }
                }
            }
        }
        private void alertallGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv.Name == "alertallGV")
                {
                    if (dgv.CurrentCell.OwningColumn.Name == "AlertallCommentOut")
                    {
                        this.alertallGV_dataGridViewComboBox =
                            (DataGridViewComboBoxEditingControl)e.Control;
                        this.alertallGV_dataGridViewComboBox.SelectedIndexChanged +=
                            new EventHandler(dataGridViewComboBox_SelectedIndexChanged);
                    }
                }
            }    
        }
        private void dataGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewComboBoxEditingControl)
            {
                DataGridView dgv = (DataGridView)sender;

                if (dgv.Name == "dataGV")
                {
                    if (dgv.CurrentCell.OwningColumn.Name == "CommentOut")
                    {
                        //listBox1.Items.Insert(0,"dataGV:CommentOut");
                        this.dataGV_dataGridViewComboBox =
                            (DataGridViewComboBoxEditingControl)e.Control;
                        this.dataGV_dataGridViewComboBox.SelectedIndexChanged +=
                            new EventHandler(dataGridViewComboBox_SelectedIndexChanged);

                    }
                }
            }
        }

        private void alertallGV_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            alertallGV_dataGridViewComboBox = null;
        }

        private void dataGV_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            dataGV_dataGridViewComboBox = null;
        }
        private void headerGV_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            headerGV_dataGridViewComboBox = null;
        }

        private void dataGridViewComboBox_SelectedIndexChanged(object sender,EventArgs e)
        {
            //選択されたアイテムを表示
            DataGridViewComboBoxEditingControl cb =(DataGridViewComboBoxEditingControl)sender;
            Point cellPositoin = ((System.Windows.Forms.DataGridView)cb.Parent.Parent).CurrentCellAddress;
            if (cb.Parent.Parent.Name == "headerGV")
            {
                if (cb.Text == "#")
                {
                    //コメントアウトにする
                    if (headerGV[HEADER_TITLE, cellPositoin.Y].Value.ToString()[0] != '#')
                    {

                        HeaderAnalyze('#' + ExportHederText(cellPositoin.Y), cellPositoin.Y);
                    }
                }
                else
                {
                    //コメントアウトを外す
                    if (headerGV[HEADER_TITLE, cellPositoin.Y].Value.ToString()[0] == '#')
                    {
                        if(HeaderAnalyze(headerGV[HEADER_TITLE, cellPositoin.Y].Value.ToString().Substring(1), cellPositoin.Y)==false)
                        {
                            HeaderAnalyze('#' + ExportHederText(cellPositoin.Y), cellPositoin.Y);
                            cb.Text = "#";
                        }
                    }
                }
            }
            if (cb.Parent.Parent.Name == "alertallGV")
            {
                if (cb.Text == "#")
                {
                    //コメントアウトにする
                    if (alertallGV[HEADER_ALERTALL_TITLE, cellPositoin.Y].Value.ToString()[0] != '#')
                    {

                        HeaderAnalyze('#' + ExportalertallText(cellPositoin.Y), cellPositoin.Y);
                    }
                }
                else
                {
                    //コメントアウトを外す
                    if (alertallGV[HEADER_ALERTALL_TITLE, cellPositoin.Y].Value.ToString()[0] == '#')
                    {
                        HeaderAnalyze(alertallGV[HEADER_ALERTALL_TITLE, cellPositoin.Y].Value.ToString().Substring(1), cellPositoin.Y);
                    }
                }
            }
            if (cb.Parent.Parent.Name == "dataGV") {

               
                if (cb.Text == "#")
                {
                    //コメントアウトにする
                    if (dataGV[DATA_TITLE, cellPositoin.Y].Value.ToString()[0] != '#')
                    {
                        
                        Analyze('#'+ ExportText(cellPositoin.Y), cellPositoin.Y);
                    }
                }
                else
                {
                    //コメントアウトを外す
                    if (dataGV[DATA_TITLE, cellPositoin.Y].Value.ToString()[0] == '#')
                    {
                       
                        Analyze(dataGV[DATA_TITLE, cellPositoin.Y].Value.ToString().Substring(1), cellPositoin.Y);
                    }
                }
            }





        }

        //Panel Move Window
        private Point mousePoint;
        private void panelTop_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                //位置を記憶する
                mousePoint = new Point(e.X, e.Y);
            }
        }

        private void panelTop_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                this.Left += e.X - mousePoint.X;
                this.Top += e.Y - mousePoint.Y;                
            }
        }

        private void LogForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (swResize)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }
            swResize = false;
        }

        bool swResize = false;
        private void btResize_Click(object sender, EventArgs e)
        {
            if (!swResize)
            {
                this.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                swResize = true;
            }else
            {
                this.FormBorderStyle = FormBorderStyle.None;
                swResize = false;
            }
        }

        private void statusStrip1_Click(object sender, EventArgs e)
        {
            btResize_Click(sender, e);
        }




        //Menu
        private void toolStripMenuItem_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ToolStripMenuItemOpenTimeLine_Click(object sender, EventArgs e)
        {
            if (openFLD.ShowDialog() == DialogResult.OK)
            {
                dataGV.RowCount = 1;
                ReadTextFile(openFLD.FileName);
            }
        }

        private void toolStripMenuItemSaveAsTimeLine_Click(object sender, EventArgs e)
        {
            if(saveFLD.ShowDialog() == DialogResult.OK)
            {
                SaveTextFile(saveFLD.FileName);
            }
        }

        private void ToolStripMenuItemSaveTimeLine_Click(object sender, EventArgs e)
        {
            if(System.IO.File.Exists(saveFLD.FileName))
            {
                SaveTextFile(saveFLD.FileName);
                return;
            }else
            {
                if (System.IO.File.Exists(openFLD.FileName))
                {
                    SaveTextFile(saveFLD.FileName);
                    return;
                }
            }
            toolStripMenuItemSaveAsTimeLine_Click(sender,e);
        }

        //Drag and Drop

        private Rectangle dragBoxFromMouseDown;
        private int rowIndexFromMouseDown;
        private int rowIndexOfItemUnderMouseToDrop;


        private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                // If the mouse moves outside the rectangle, start the drag.
                if (dragBoxFromMouseDown != Rectangle.Empty &&
                !dragBoxFromMouseDown.Contains(e.X, e.Y))
                {
                    DataGridView grid = (DataGridView)sender;
                    // Proceed with the drag and drop, passing in the list item.                    
                    DragDropEffects dropEffect = grid.DoDragDrop(
                    grid.Rows[rowIndexFromMouseDown],
                    DragDropEffects.Move);
                }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            // Get the index of the item the mouse is below.
            rowIndexFromMouseDown = grid.HitTest(e.X, e.Y).RowIndex;
            if (rowIndexFromMouseDown != -1)
            {
                // Remember the point where the mouse down occurred. 
                // The DragSize indicates the size that the mouse can move 
                // before a drag event should be started.                
                Size dragSize = SystemInformation.DragSize;
                // Create a rectangle using the DragSize, with the mouse position being
                // at the center of the rectangle.
                dragBoxFromMouseDown = new Rectangle(new Point(e.X - (dragSize.Width / 2),
                e.Y - (dragSize.Height / 2)),
                dragSize);
            }
            else
                // Reset the rectangle if the mouse is not over an item in the ListBox.
                dragBoxFromMouseDown = Rectangle.Empty;
        }

        private void dataGridView1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            // The mouse locations are relative to the screen, so they must be 
            // converted to client coordinates.
            Point clientPoint = grid.PointToClient(new Point(e.X, e.Y));
            // Get the row index of the item the mouse is below. 
            rowIndexOfItemUnderMouseToDrop =
            grid.HitTest(clientPoint.X, clientPoint.Y).RowIndex;
            // If the drag operation was a move then remove and insert the row.
            if (e.Effect == DragDropEffects.Move)
            {
                DataGridViewRow oneRow = grid.Rows[rowIndexFromMouseDown];
                grid.Rows.RemoveAt(rowIndexFromMouseDown);
                grid.Rows.Insert(rowIndexOfItemUnderMouseToDrop, oneRow);

                //符番
                AutoIdCreate();

            }

        }

        private void headerGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex >-1 && e.RowIndex > -1)
            {
                DataGridView dgv = (DataGridView)sender;
                if (dgv[e.ColumnIndex, e.RowIndex].Value != null)
                {
                    toolStripStatusLb.Text = dgv[e.ColumnIndex, e.RowIndex].Value.ToString();
                }                
            }
        }
        //サウンドを追加する
        private void addSoundMenuClick(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridViewRow dgr = this.dataGV.CurrentRow;

            DataGridViewTextBoxCell text = (DataGridViewTextBoxCell)dgr.Cells[DATA_TITLE];
            AddAlertall(text.Value.ToString());

        }
        private void AddAlertall(string Title)
        {
            //alertall アトミックレイ before 0 speak "Slow" "吉田アアアアアアアアアアアアアアアアアアアアア"

            alertallGV.RowCount++;
            int row = alertallGV.RowCount - 2;

            HeaderAnalyze("alertall " + Title + " before 0 speek "+'"'+ "Normal" +'"' + ' '+　'"' + Title+'"',row);

            AutoIdCreate();
        }

        private void GV_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            AutoIdCreate();
        }

        //XML OutPut
        private void ToolStripMenuItemExportXML_Click(object sender, EventArgs e)
        {

        }
        //DBConnect
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;
            SqlCommand command = null;
            DataTable dt = new DataTable();
            var connectionString = GetConnectionString();
            try
            {
                // データベース接続の準備
                connection = new SqlConnection(connectionString);

                // データベースの接続開始
                connection.Open();

                // SQLの実行
                command = new SqlCommand()
                {
                    Connection = connection,
                    CommandText = SQL_CHACK_DB
                };
                SqlDataReader reader = command.ExecuteReader();

                string name;
                while (reader.Read())
                {
                    name = (string)reader.GetValue(0);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
            finally
            {
                // データベースの接続終了
                connection.Close();
            }
        }

        const string SQL_CHACK_DB = @"SELECT NAME FROM SYS.DATABASES WHERE NAME='multi_project'";
        public string GetConnectionString()
        {
            var builder = new SqlConnectionStringBuilder()
            {
                DataSource = "FFF",
                IntegratedSecurity = true,
            };
            return builder.ToString();
        }

    }
}
