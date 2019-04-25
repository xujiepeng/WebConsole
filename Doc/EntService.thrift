namespace csharp entService.Contract

service EntService{    
   list<TableStatusStru> GetTableStatus(1:map<string, string> Param)
   list<TaskRunLogStru> GetTaskRunLog(1:map<string, string> Param)
	 list<TaskDriverStru> GetTaskDriver(1:map<string, string> Param)
	 list<AttriConfStru> GetAttriConf(1:map<string, string> Param)
	 list<CommonConfigStru> GetCommonConfig(1:map<string, string> Param)
	 list<DbColTypeConfigStru> GetDbColTypeConfig(1:map<string, string> Param)
	 list<DbTableColConfigStru> GetDbTableColConfig(1:map<string, string> Param)
	 list<DbTableConfigStru> GetDbTableConfig(1:map<string, string> Param)
	 list<DbTableIndexConfigStru> GetDbTableIndexConfig(1:map<string, string> Param)
	 list<DownLoadAccountStru> GetDownLoadAccount(1:map<string, string> Param)
	 list<InterfaceConfStru> GetInterfaceConf(1:map<string, string> Param)
	 list<KeepAliveStru> GetKeepAlive()
	 SaveResult SetDownLoadAccount(1:DownLoadAccountStru Account)
	 SaveResult SetSfpt(1:CommonConfigStru Sfpt)
	 SaveResult SethttpAddr(1:CommonConfigStru httpAddr)
	 SaveResult SetTaskDriver(1:TaskDriverStru Driver)
	 SaveResult ExtensionMathod(1:string extensionCode)
} 

enum SaveResult {     
  SUCCESS = 1,  
  FAILED = 0,  
}  

struct TableStatusStru {
    1: optional string ID;
		2: optional string taskId;
		3: optional string lastStartTime;
		4: optional string lastEndTime;
		5: optional string nextRunTime;
		6: optional string taskStatus;
		7: optional string startTmStamp;
		8: optional string endTmStamp;
		9: optional i32 querySpan;
		10: optional i32 tranSpan;
		11: optional i32 writeSpan;
		12: optional i32 totalDataPacketNum;
		13: optional i32 FinishDataPacketNum;
		14: optional i32 totalRecordNum;
		15: optional i32 finishRecordNum;
		16: optional string updateTime;
		17: optional string userName;
		18: optional string tableName;
}

struct TaskDriverStru {
    1: optional string Id;
		2: optional string taskId;
		3: optional string userName;
		4: optional string taskName;
		5: optional string Tables;
		6: optional string taskStatus;
		7: optional string TMSP1;
		8: optional string TMSP2;
		9: optional string TMSP3;
		10: optional bool Init;
}

struct TaskRunLogStru {
    1: optional string ID;
		2: optional string userName;
		3: optional string taskID;
		4: optional string level;
		5: optional string recordTime;
		6: optional string Info;	
}

struct AttriConfStru {
    1: optional string ID;
		2: optional string confName;
		3: optional string Value;
		4: optional string Describe;
		5: optional string Ver;
		6: optional string isEnable;	
		7: optional string entryDate;	
}

struct CommonConfigStru {
    1: optional string Name;
		2: optional string ftpUri;
		3: optional string ftpPort;
		4: optional string ftpUserID;
		5: optional string ftpPassword;
		6: optional bool isEncrypt;	
		7: optional string httpsAddr;	
}

struct DbColTypeConfigStru {
    1: optional string Id;
		2: optional string DBType;
		3: optional string ColId;
		4: optional string defaultValue;
		5: optional string ColType;
		6: optional string ColLength;
		7: optional string Ver;
		8: optional string isEnable;
		9: optional string entryDate;
		10: optional string ColName;
		11: optional string TableName;
}

struct DbTableColConfigStru {
    1: optional string Id;
		2: optional string tableId;
		3: optional string colName;
		4: optional string iColName;
		5: optional string ColName_CN;
		6: optional string isPk;
		7: optional string isIgnore;
		8: optional string isKey;
		9: optional string sortCol;
		10: optional string isNotNull;
		11: optional string Ver;
		12: optional string isEnable;
		13: optional string entryDate;
		14: optional string TableName;
}

struct DbTableConfigStru {
    1: optional string Id;
		2: optional string tableName;
		3: optional string tableName_CN;
		4: optional string Iname;
		5: optional string tableType;
		6: optional string dataFilter;
		7: optional string isOrigin;
		8: optional string Ver;
		9: optional string isEnable;
		10: optional string entryDate;
}

struct DbTableIndexConfigStru {
    1: optional string Id;
		2: optional string indexName;
		3: optional string tableId;
		4: optional string indexColName;
		5: optional string Ver;
		6: optional string isEnable;
		7: optional string entryDate;
		8: optional string TableName;
}

struct DownLoadAccountStru {
    1: optional string userName;
		2: optional string Pwd;
		3: optional bool pwdIsEncrypt;
		4: optional string Name;
		5: optional string DbType;
		6: optional string ConnStr;
		7: optional bool connStrIsEncrypt;
		8: optional string Encoding;
}

struct InterfaceConfStru {
    1: optional string Id;
		2: optional string confName;
		3: optional string Interface;
		4: optional string reqType;
		5: optional string param;
		6: optional string Describe;
		7: optional string Ver;
		8: optional string isEnable;
		9: optional string entryDate;
}

struct KeepAliveStru {
    1: optional string Id;
		2: optional string keepAliveTime;
}