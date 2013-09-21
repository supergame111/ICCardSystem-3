using System;
//�������
using System.Data;
using System.Data.OleDb;

namespace HID.BaseClass
{
    class DBOperation
    {
        private OleDbConnection oledCon;

        #region ���--IC_Data
        public int Add_IcData(cIC_Card_Data icData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strAdd = "insert into tb_ICCard_Data (IC_ID,д��ʱ��,Block_8,Block_9,Block_10,����,�ֿ���,����ID)";
                //string strAdd = "insert into tb_ICCard_Data (IC_ID,д��ʱ��,Block_8,Block_9,Block_10)";
                strAdd += "values('" + icData.IC_ID + "','" + DateTime.Now.ToString() + "','" + 
                    BitConverter.ToString(icData.Block_8) + "','" + 
                    BitConverter.ToString(icData.Block_9)+"','" +
                    BitConverter.ToString(icData.Block_10) + "','" +
                    icData.ic_num+"','" +
                    icData.�ֿ���+"','" +
                    icData.����ID+"')" 
                    ;
                //strAdd += "values('" + icData.IC_ID + "','" + icData.IC_Fun + "',";
                //strAdd += "'" + icData.Floor_Data + "','" + icData.IC_FunA + "','" + icData.Add_ID + "')";
                OleDbCommand oledCmd = new OleDbCommand(strAdd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ����--IC_Data
        public int Updata_IcData(cIC_Card_Data icData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strAdd = "update tb_ICCard_Data ";
                strAdd +=   "set д��ʱ��='" + DateTime.Now.ToString()+"'," +
                            "Block_8='" + BitConverter.ToString(icData.Block_8) + "'," +
                            "Block_9='" + BitConverter.ToString(icData.Block_9) + "'," +
                            "Block_10='" + BitConverter.ToString(icData.Block_10) + "'," +
                            "����='" + icData.ic_num + "'," +
                            "�ֿ���='" + icData.�ֿ��� + "'," +
                            "����ID='" + icData.����ID + "'" +
                            "where IC_ID='"+ icData.IC_ID+"'" ;
 
                OleDbCommand oledCmd = new OleDbCommand(strAdd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ��������--IC_Data
        public int Find_IcData(cIC_Card_Data icData)
        {
            int intFalg = 0;
            try
            {
                string strcmd = "select * from tb_ICCard_Data where IC_ID='" + icData.IC_ID+"'" ;

                DataSet ds = DBHelper.getDs(strcmd, "tb_Data");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    intFalg = 1;

                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ɾ������--IC_Data
        public int Delete_IcData(cIC_Card_Data icData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strcmd = "delete from tb_ICCard_Data where IC_ID='" + icData.IC_ID+"'" ;

                OleDbCommand oledCmd = new OleDbCommand(strcmd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ���--MainBoard_Data
        public int Add_MainBoard_Data(s_MainBoard_Data mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strAdd = "insert into tb_MainBoard (MainBoard_ID,MainBoard_Num,��λ����)";
                strAdd += "values('" + mbData.MainBoard_ID + "','" + mbData.MainBoard_Num + "','" +
                    mbData.Company_Name + "')";

                OleDbCommand oledCmd = new OleDbCommand(strAdd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ����--MainBoard_Data
        public int Updata_MainBoard_Data(s_MainBoard_Data mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strAdd = "update tb_MainBoard ";
                strAdd += "set MainBoard_Num='" + mbData.MainBoard_Num + "'" + "set ��λ����='" + mbData.Company_Name + "'" + "where MainBoard_ID='" + mbData.MainBoard_ID + "'";

                OleDbCommand oledCmd = new OleDbCommand(strAdd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//��ӳɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ��������--MainBoard_Data
        public int Find_MainBoard_Data(s_MainBoard_Data mbData)
        {
            int intFalg = 0;
            try
            {
                //oledCon = data.getCon();
                //oledCon.Open();
                string strcmd = "select * from tb_MainBoard where MainBoard_ID='" + mbData.MainBoard_ID + "'";

                DataBase data = new DataBase();//����DataBase��Ķ���
                DataSet ds = data.getDs(strcmd, "tb_MB");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    intFalg = 1;

                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion

        #region ɾ������--MainBoard_Data
        public int Delete_MainBoard_Data(s_MainBoard_Data mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                oledCon.Open();
                string strcmd = "delete from tb_MainBoard where MainBoard_ID='" + mbData.MainBoard_ID + "'";

                OleDbCommand oledCmd = new OleDbCommand(strcmd, oledCon);
                if (oledCmd.ExecuteNonQuery() != 0)
                {
                    intFalg = 1;//�ɹ�
                }
                return intFalg;
            }
            catch (Exception ee)
            {
                return intFalg;
            }
        }
        #endregion
    }

    #region ���� IC_Card_Data ���ݽṹ
    public class cIC_Card_Data
    {
        private string ic_id = "";
        private string ic_fun = "";
        private string floor_data = "";
        private string ic_funa = "";
        private int _ic_num ;
        private string _�ֿ��� = "";
        private string _����ID = "";
        private byte[] block_8=new byte[16];
        private byte[] block_9 = new byte[16];
        private byte[] block_10 = new byte[16];
        //private byte[] block_9;
        //private byte[] block_10;

        public string �ֿ���
        {
            get { return _�ֿ���; }
            set { _�ֿ��� = value; }
        }

        public string ����ID
        {
            get { return _����ID; }
            set { _����ID = value; }
        }

        public string IC_ID
        {
            get { return ic_id; }
            set { ic_id = value; }
        }
        /// <summary>
        /// ��λȫ��
        /// </summary>
        public string IC_Fun
        {
            get { return ic_fun; }
            set { ic_fun = value; }
        }
        /// <summary>
        /// ��λ˰��
        /// </summary>
        public string Floor_Data
        {
            get { return floor_data; }
            set { floor_data = value; }
        }
        /// <summary>
        /// ��ϵ��
        /// </summary>
        public string IC_FunA
        {
            get { return ic_funa; }
            set { ic_funa = value; }
        }
        /// <summary>
        /// ��ϵ�绰
        /// </summary>
        public int ic_num
        {
            get { return _ic_num; }
            set { _ic_num = value; }
        }

        public byte[] Block_8
        {
            get { return block_8; }
            set { block_8 = value; }
        }

        public byte[] Block_9
        {
            get { return block_9; }
            set { block_9 = value; }
        }

        public byte[] Block_10
        {
            get { return block_10; }
            set { block_10 = value; }
        }
   
    }
    #endregion
   
    #region ���� MainBoard_Data
    public class s_MainBoard_Data
    {
        private string _MainBoard_ID = "";
        private string _MainBoard_Num = "";
        private string _Company_Name = "";

        public string MainBoard_ID
        {
            get { return _MainBoard_ID; }
            set { _MainBoard_ID = value; }
        }

        public string MainBoard_Num
        {
            get { return _MainBoard_Num; }
            set { _MainBoard_Num = value; }
        }

        public string Company_Name
        {
            get { return _Company_Name; }
            set { _Company_Name = value; }
        }
    }
    #endregion
    
}