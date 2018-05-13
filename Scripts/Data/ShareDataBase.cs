using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Mono.Data.Sqlite;

//封装可以存图片或对象的 SQLite时使用
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System;


public class ShareDataBase
{
	// 单例类 【区分单利类，单例脚本组件，静态工具类】
	public static ShareDataBase sDb = new ShareDataBase ();

	// 数据库连接对象
	SqliteConnection con;
	// 数据库指令对象
	SqliteCommand command;
	// 查询结果集对象
	SqliteDataReader reader;
	// 重写构造函数, 并且连接数据库/实例化执行序列
	private ShareDataBase ()
	{
        //打包发布的各个平台路径【跟不同平台读取数据的路径区分开】
#if UNITY_EDITOR
		// StreamingAssets路径
		string dataPath = "Data Source = " + Application.streamingAssetsPath + "/SQLite/MoonNight.db";
#endif
        try
        {
			if (con == null) {
				con = new SqliteConnection (dataPath);
			}
		} catch (SqliteException ex) {
			Debug.Log (ex);	
		}
        //单利赋值
        sDb = this;
    }


    // 打开数据库
    private void OpenConnectDataBase ()
	{
		try {
			con.Open ();
			//每一次打开数据库时再创建这个SQLite指令，关闭时释放掉
			command = con.CreateCommand ();
		} catch (SqliteException ex) {
    
		}
	}
	// 关闭数据库
	private void CloseConnectionDataBase ()
	{
		try {
			command.Dispose ();
			con.Close ();//关闭数据库
			con.Dispose ();//释放内存资源
		} catch (SqliteException ex) {
			Debug.Log (ex);
		}
	}

    // 
    /// <summary>
    /// 增-删-改的操作方法
    /// </summary>
    /// <param name="query"></param>
    public void ExecSql (string query)
	{
		OpenConnectDataBase ();
		try {
			command.CommandText = query;
			command.ExecuteNonQuery ();
			//command.Dispose ();//这里不需要释放内存资源写在关闭数据库中省事
		} catch (SqliteException ex) {
			Debug.Log (ex);
		}
		CloseConnectionDataBase ();
	}

	// 返回第一个单元格(就是一个字段对应的数据)
	public object SelectFiledSql (string query)
	{
		OpenConnectDataBase ();
		object obj = new object ();
		try {
			command.CommandText = query;
			obj = command.ExecuteScalar ();
			//command.Dispose ();//这里不需要释放内存资源写在关闭数据库中省事
		} catch (SqliteException ex) {
			Debug.Log (ex);
		}
		CloseConnectionDataBase ();
		return obj;
	}


	// 返回全部结果集, 需要外部继续解析.
	public List<ArrayList> SelectResultSql (string query)
	{
		OpenConnectDataBase ();
		//每一条数据中可能有多个不同类型的字段，查询的到可能不止一条数据
		List <ArrayList> list = new List<ArrayList> ();
		try {
			command.CommandText = query;
			reader = command.ExecuteReader ();
			while (reader.Read ()) {
				ArrayList alist = new ArrayList ();
				for (int i = 0; i < reader.FieldCount; i++) {
					alist.Add (reader.GetValue (i));
				}
				list.Add (alist);
			}
            //记得释放
			reader.Close ();
			reader.Dispose ();
			//command.Dispose ();//这里不需要释放内存资源写在关闭数据库中省事
		} catch (SqliteException ex) {
			Debug.Log (ex);
		}
		CloseConnectionDataBase ();
		return list;
	}













	////////////////////////以下功能是往 SQLite 中封装存储和获取图片或任意对象方法//////////////////////

	// 将一个object对象序列化, 返回一个byte[]
	public byte[] ObjectToBytes (object obj)
	{
		using (MemoryStream ms = new MemoryStream ()) {
			IFormatter formatter = new BinaryFormatter ();
			formatter.Serialize (ms, obj);
			return ms.GetBuffer ();
		}
	}

	// 将一个byte[]转换为一个object
	public object BytesToObject (byte[] Bytes)
	{
		using (MemoryStream ms = new MemoryStream (Bytes)) {
			IFormatter formatter = new BinaryFormatter ();
			return formatter.Deserialize (ms);
		}
	}

	//可以存储图片或者任意对象转的数据流 【结合上面的对象转数据流和对应的数据流转对象方法】
	public void InsertData (string name, int age, byte[] info)
	{
		OpenConnectDataBase ();
		//将传进来的信息绑定到对应数据流信息中
		string sql = String.Format ("insert into tmp1(name, age, info) values (@name, @age,@info)");
		//绑定信息
		SqliteParameter[] parameters = {
			new SqliteParameter ("@name", name),
			new SqliteParameter ("@age", age.ToString ()),
			new SqliteParameter ("@info", info)
		};
		//存储数据流
		command.Parameters.AddRange (parameters);
		command.CommandText = sql;
		command.ExecuteNonQuery ();
		//command.Dispose ();
		CloseConnectionDataBase ();
	}


}
