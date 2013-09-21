using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//�������
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace HID.BaseClass
{
    class DataBase:IDisposable
    {
        private static SqlConnection con;  //�������Ӷ���
        OleDbConnection oledbcon;
        OleDbCommand oledbcom;
        OleDbDataAdapter oledbda;
        DataSet ds;

        #region   �����ݿ�����
        /// <summary>
        /// �����ݿ�����.
        /// </summary>
        private void Open()
        {
            string strDPath = Application.StartupPath;
            string strDataSource = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + strDPath.Substring(0, strDPath.LastIndexOf("\\")).Substring(0, strDPath.Substring(0, strDPath.LastIndexOf("\\")).LastIndexOf("\\")) + "\\DataBase\\db_Login.mdb";

            if (con == null)//�ж����Ӷ����Ƿ�Ϊ��
            {
                //con = new SqlConnection("Data Source=MRKJ_ZHD\\EAST;DataBase=db_EMS;User ID=sa;PWD=111");//�������ݿ����Ӷ���
                con = new SqlConnection(strDataSource);//�������ݿ����Ӷ���
            }
            if (con.State == System.Data.ConnectionState.Closed)//�ж����ݿ������Ƿ�ر�
                con.Open();//�����ݿ�����
        }
        #endregion

        #region  �ر�����
        /// <summary>
        /// �ر����ݿ�����
        /// </summary>
        public void Close()
        {
            if (con != null)//�ж����Ӷ����Ƿ�Ϊ��
                con.Close();//�ر����ݿ�����
        }
        #endregion

        #region �ͷ����ݿ�������Դ
        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void Dispose()
        {
            if (con != null)//�ж����Ӷ����Ƿ�Ϊ��
            {
                con.Dispose();//�ͷ����ݿ�������Դ
                con = null;//�������ݿ�����Ϊ��
            }
        }
        #endregion

        #region   �����������ת��ΪSqlParameter����
        /// <summary>
        /// ת������
        /// </summary>
        /// <param name="ParamName">�洢�������ƻ������ı�</param>
        /// <param name="DbType">��������</param></param>
        /// <param name="Size">������С</param>
        /// <param name="Value">����ֵ</param>
        /// <returns>�µ� parameter ����</returns>
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);//����SQL����
        }

        /// <summary>
        /// ��ʼ������ֵ
        /// </summary>
        /// <param name="ParamName">�洢�������ƻ������ı�</param>
        /// <param name="DbType">��������</param>
        /// <param name="Size">������С</param>
        /// <param name="Direction">��������</param>
        /// <param name="Value">����ֵ</param>
        /// <returns>�µ� parameter ����</returns>
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;//����SQL��������
            if (Size > 0)//�жϲ����ֶ��Ƿ����0
                param = new SqlParameter(ParamName, DbType, Size);//����ָ�������ͺʹ�С����SQL����
            else
                param = new SqlParameter(ParamName, DbType);//����ָ�������ʹ���SQL����
            param.Direction = Direction;//����SQL����������
            if (!(Direction == ParameterDirection.Output && Value == null))//�ж��Ƿ�Ϊ�������
                param.Value = Value;//���ò�������ֵ
            return param;//����SQL����
        }
        #endregion

        #region   ִ�в��������ı�(�����ݿ������ݷ���)
        /// <summary>
        /// ִ������
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <param name="prams">��������</param>
        /// <returns></returns>
        public int RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);//����SqlCommand�������
            cmd.ExecuteNonQuery();//ִ��SQL����
            this.Close();//�ر����ݿ�����
            return (int)cmd.Parameters["ReturnValue"].Value;//�õ�ִ�гɹ�����ֵ
        }
        /// <summary>
        /// ֱ��ִ��SQL���
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <returns></returns>
        public int RunProc(string procName)
        {
            this.Open();//�����ݿ�����
            SqlCommand cmd = new SqlCommand(procName, con);//����SqlCommand�������
            cmd.ExecuteNonQuery();//ִ��SQL����
            this.Close();//�ر����ݿ�����
            return 1;//����1����ʾִ�гɹ�
        }

        #endregion

        #region   ִ�в��������ı�(�з���ֵ)
        /// <summary>
        /// ִ�в�ѯ�����ı������ҷ���DataSet���ݼ�
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <param name="prams">��������</param>
        /// <param name="tbName">���ݱ�����</param>
        /// <returns></returns>
        public DataSet RunProcReturn(string procName, SqlParameter[] prams,string tbName)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, prams);//�����Ž�������
            DataSet ds = new DataSet();//�������ݼ�����
            dap.Fill(ds, tbName);//������ݼ�
            this.Close();//�ر����ݿ�����
            return ds;//�������ݼ�
        }

        /// <summary>
        /// ִ�������ı������ҷ���DataSet���ݼ�
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <param name="tbName">���ݱ�����</param>
        /// <returns>DataSet</returns>
        public DataSet RunProcReturn(string procName, string tbName)
        {
            SqlDataAdapter dap = CreateDataAdaper(procName, null);//�����Ž�������
            DataSet ds = new DataSet();//�������ݼ�����
            dap.Fill(ds, tbName);//������ݼ�
            this.Close();//�ر����ݿ�����
            return ds;//�������ݼ�
        }

        #endregion

        #region �������ı���ӵ�SqlDataAdapter
        /// <summary>
        /// ����һ��SqlDataAdapter�����Դ���ִ�������ı�
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <param name="prams">��������</param>
        /// <returns></returns>
        private SqlDataAdapter CreateDataAdaper(string procName, SqlParameter[] prams)
        {
            this.Open();//�����ݿ�����
            SqlDataAdapter dap = new SqlDataAdapter(procName, con);//�����Ž�������
            dap.SelectCommand.CommandType = CommandType.Text;//ָ��Ҫִ�е�����Ϊ�����ı�
            if (prams != null)//�ж�SQL�����Ƿ�Ϊ��
            {
                foreach (SqlParameter parameter in prams)//�������ݵ�ÿ��SQL����
                    dap.SelectCommand.Parameters.Add(parameter);//��SQL������ӵ�ִ�����������
            }
            //���뷵�ز���
            dap.SelectCommand.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,string.Empty, DataRowVersion.Default, null));
            return dap;//�����Ž�������
        }
        #endregion

        #region   �������ı���ӵ�SqlCommand
        /// <summary>
        /// ����һ��SqlCommand�����Դ���ִ�������ı�
        /// </summary>
        /// <param name="procName">�����ı�</param>
        /// <param name="prams"�����ı��������</param>
        /// <returns>����SqlCommand����</returns>
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            this.Open();//�����ݿ�����
            SqlCommand cmd = new SqlCommand(procName, con);//����SqlCommand�������
            cmd.CommandType = CommandType.Text;//ָ��Ҫִ�е�����Ϊ�����ı�
            // ���ΰѲ������������ı�
            if (prams != null)//�ж�SQL�����Ƿ�Ϊ��
            {
                foreach (SqlParameter parameter in prams)//�������ݵ�ÿ��SQL����
                    cmd.Parameters.Add(parameter);//��SQL������ӵ�ִ�����������
            }
            //���뷵�ز���
            cmd.Parameters.Add(new SqlParameter("ReturnValue", SqlDbType.Int, 4,
                ParameterDirection.ReturnValue, false, 0, 0,string.Empty, DataRowVersion.Default, null));
            return cmd;//����SqlCommand�������
        }
        #endregion

        public OleDbConnection getCon()
        {
            string strDPath = Application.StartupPath;
            string strDataSource = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
                + strDPath + "\\DataBase\\db_Login.mdb;" + "Jet OLEDB:Database Password=xzy;";
            //string strDataSource = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source="
            //    + strDPath.Substring(0, strDPath.LastIndexOf("\\")).Substring(0, strDPath.Substring(0, strDPath.LastIndexOf("\\")).LastIndexOf("\\")) + "\\DataBase\\db_Login.mdb;" + "Jet OLEDB:Database Password=xzy;";
            OleDbConnection oledbCon = new OleDbConnection(strDataSource);
            return oledbCon;
        }

        public void getCom(string strCon)
        {
            oledbcon = getCon();
            oledbcom = new OleDbCommand(strCon, oledbcon);
            oledbcon.Open();
            oledbcom.ExecuteNonQuery();
            oledbcon.Close();
        }

        public DataSet getDs(string strCon, string tbname)
        {
            oledbcon = getCon();
            oledbda = new OleDbDataAdapter(strCon, oledbcon);
            ds = new DataSet();
            oledbda.Fill(ds, tbname);
            return ds;
        }
    }
}
