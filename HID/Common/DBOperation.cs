using System;
//�������
using System.Data;
using System.Data.OleDb;
using HID.Model;

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

                string strAdd = "insert into tb_ICCard_Data (IC_ID,д��ʱ��,Block_8,Block_9,Block_10,����,����ID,�ֿ���)";  //
                strAdd += " values('" + icData.IC_ID + "','" + DateTime.Now.ToString() + "','" +
                    BitConverter.ToString(icData.Block_8) + "','" +
                    BitConverter.ToString(icData.Block_9) + "','" +
                    BitConverter.ToString(icData.Block_10) + "','" +
                    icData.ic_num + "','" +
                    icData.����ID + "','" +
                    icData.�ֿ��� + "')";

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
                string strAdd = "update tb_ICCard_Data ";
                strAdd +=   "set д��ʱ��='" + DateTime.Now.ToString()+"'," +
                            "Block_8='" + BitConverter.ToString(icData.Block_8) + "'," +
                            "Block_9='" + BitConverter.ToString(icData.Block_9) + "'," +
                            "Block_10='" + BitConverter.ToString(icData.Block_10) + "'," +
                            "����='" + icData.ic_num + "'," +
                            "����ID='" + icData. ����ID+ "'," +
                            "�ֿ���='" + icData.�ֿ��� + "'" +
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
        public int Add_MainBoard_Data(MainBoard mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                string strAdd = "insert into tb_MainBoard (MainBoard_ID,MainBoard_Num,��λ����)";
                strAdd += "values('" + mbData.Id + "','" + mbData.Number + "','" +
                    mbData.CompanyName + "')";

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
        public int Updata_MainBoard_Data(MainBoard mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                string strAdd = "update tb_MainBoard ";
                strAdd += "set MainBoard_Num='" + mbData.Number+ "'" + "set ��λ����='" + 
                    mbData.CompanyName + "'" + "where MainBoard_ID='" + mbData.Id + "'";

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
        public int Find_MainBoard_Data(MainBoard mbData)
        {
            int intFalg = 0;
            try
            {
                //oledCon = data.getCon();
                string strcmd = "select * from tb_MainBoard where MainBoard_ID='" + mbData.Id + "'";

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
        public int Delete_MainBoard_Data(MainBoard mbData)
        {
            int intFalg = 0;
            try
            {
                oledCon = DBHelper.getCon();
                string strcmd = "delete from tb_MainBoard where MainBoard_ID='" + mbData.Id + "'";

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
}