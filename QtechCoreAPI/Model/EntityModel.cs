using System;
using System.Collections.Generic;

namespace QtechCoreAPI
{
    public class cameraQCVoList
    {
        public long id { get; set; }
        public string supplierName { get; set; }
        public string serialNumber { get; set; }
        public string line { get; set; }
        public string testStation { get; set; }
        public string factoryName { get; set; }
        public string testType { get; set; }
        public string lotNumber { get; set; }
        public string equipmentNumber { get; set; }
        public string programVersion { get; set; }
        public string partNumber { get; set; }
        //public long testTime { get; set; }
        public string barcode { get; set; }
        public string partspec { get; set; }
        public string mac { get; set; }
        public string ip { get; set; }
        public string empid { get; set; }
        public string wo { get; set; }
        public string moduleid { get; set; }
        public string opcode { get; set; }
        public string result { get; set; }
        public string linkmoduleid { get; set; }


        public string deviceid { get; set; }
        public string ngtype { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public string memo3 { get; set; }
        public string memo4 { get; set; }
        public string memo5 { get; set; }
        public string GuId { get; set; }
        public int ReyCount { get; set; }
        public List<testItemList> testItemList { get; set; }//数组处理       
        public IEnumerable<testItemList> TestList { get; set; }//数组处理       
    };
    public class testItemList
    {
        public string testItemName { get; set; }
        public List<testSubItem> testSubItem { get; set; }//数组处理      
    };
    public class testSubItem
    {
        public long id { get; set; }
        public string unit { get; set; }
        public string subItemTestResult { get; set; }
        public string specUpperLimit { get; set; }
        public string subItemTestValue { get; set; }
        public string specLowerLimit { get; set; }
        public string remark { get; set; }
        public string testSubItemName { get; set; }
        public string testCondition { get; set; }
        public DateTime testTime { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public string memo3 { get; set; }
        public string memo4 { get; set; }
        public string memo5 { get; set; }
    };
    public class TestFpDataDetail
    {
        public long id { get; set; }
        public string testItem { get; set; }
        public string testSubItem { get; set; }
        public string testCondition { get; set; }
        public string upper { get; set; }
        public string lower { get; set; }
        public string unit { get; set; }
        public string subItemTestValue { get; set; }
        public string subItemTestResult { get; set; }
        public string memo { get; set; }
        public DateTime testTime { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public string memo3 { get; set; }
        public string memo4 { get; set; }
        public string memo5 { get; set; }

    };
    public class MessageString
    {
        public string Message { get; set; }
    }
    public class CheckStationParam
    {
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// 模组ID
        /// </summary>
        public string Moduleid { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Memo1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Memo2 { get; set; }
    }
    public class Router2Op
    {
        /// <summary>
        /// 流程名称
        /// </summary>
        public string Router_Code { get; set; }
        /// <summary>
        /// 序列
        /// </summary>
        public int Op_Seq { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string Op_Code { get; set; }
    }
    public class RcardPanelPartSpec
    {
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 工单
        /// </summary>
        public string Wo_Code { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string Part_Spec { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string Customer { get; set; }
    }
    public class SnSortParam
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// 机种
        /// </summary>
        public string PartSpec { get; set; }
    }
    public class ApUrl
    {
        /// <summary>
        /// 服务IP+端口
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
    }
    public class LogMessage
    {
        /// <summary>
        /// MAC
        /// </summary>
        public string Mac { get; set; }
        /// <summary>
        /// 电脑用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否本地存储
        /// </summary>
        public string isLocation { get; set; }
    }
    public class SnPackParam
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// 箱号
        /// </summary>
        public string CatonNo { get; set; }
    }
    public class PartSpecClientModel
    {
        /// <summary>
        /// 机种
        /// </summary>
        public string PartSpec { get; set; }
        /// <summary>
        /// 客户
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 客户料号
        /// </summary>
        public string MaterialCode { get; set; }
        /// <summary>
        /// 包装日期
        /// </summary>
        public string PackDate { get; set; }
        /// <summary>
        /// 成品料号
        /// </summary>
        public string ITEM_CODE { get; set; }
    }
    public class TestModData
    {
        /// <summary>
        /// ID
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 机种
        /// </summary>
        public string PartSpec { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 测试状态
        /// </summary>
        public string IsValid { get; set; }
        /// <summary>
        /// 不良代码
        /// </summary>
        public string NgType { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
    }
    public class TestModDataDetail
    {
        /// <summary>
        /// 测试项
        /// </summary>
        public string TestItem { get; set; }
        /// <summary>
        /// 测试子项
        /// </summary>
        public string TestSubItem { get; set; }
        /// <summary>
        /// 测试值
        /// </summary>
        public string SubItemTestValue { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string SubItemTestResult { get; set; }
    }
    public class TestModOpCode
    {
        /// <summary>
        /// 机种
        /// </summary>
        public string PartSpec { get; set; }
        /// <summary>
        /// 工序序列
        /// </summary>
        public int OpSeq { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public string RouterCode { get; set; }
    }
    public class TestAF
    {
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 测试结果
        /// </summary>
        public string Judge { get; set; }
        /// <summary>
        /// 测试状态
        /// </summary>
        public string IsValid { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
    }
    public class StringParam
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
    }
    public class GenerateTrayNoParam
    {
        /// <summary>
        /// 小箱号
        /// </summary>
        public string cartonNo { get; set; }
        /// <summary>
        /// 机台号
        /// </summary>
        public string stationId { get; set; }
    }
    public class TraySnParam
    {
        /// <summary>
        /// 小箱号
        /// </summary>
        public string cartonNo { get; set; }
        /// <summary>
        /// trayNo
        /// </summary>
        public string trayNo { get; set; }
        /// <summary>
        /// 产品二维码
        /// </summary>
        public List<string> snCodes { get; set; }
        /// <summary>
        /// 机台号
        /// </summary>
        public string stationId { get; set; }
    }
    public class RcardTypeParam
    {
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 流程卡类型
        /// </summary>
        public string CheckType { get; set; }
    }
    public class SnParam
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 机台号
        /// </summary>
        public string StationId { get; set; }

    }
    public class SnInvlidParam
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string PartSpec { get; set; }
        /// <summary>
        /// 机台号
        /// </summary>
        public string StationId { get; set; }
        /// <summary>
        /// 是否解绑SN
        /// </summary>
        public string isInvlidSnLink { get; set; }
    }
    public class ToJsonFP
    {
        public string dataType { get; set; }
        public string appKey { get; set; }

        public List<cameraOTP09VoList> cameraOTP09VoList { get; set; }
    }
    public class cameraOTP09VoList
    {
        public string supplierName { get; set; }
        public string factoryName { get; set; }
        public string barCode { get; set; }
        public string deliveryNo { get; set; }
        public string count { get; set; }
        public long deliverySendTime { get; set; }

        public List<Moduleid> idList;//数组处理     
        public string jsonCount { get; set; }
        public string jsonTotal { get; set; }
        public string remark1 { get; set; }
        public string remark2 { get; set; }
    };
    public class Moduleid
    {
        public string sensorID { get; set; }
        //public long sendTime { get; set; }
    }
    public class FpResponse
    {
        public bool status { get; set; }
        public string successMsg { get; set; }
        public string errorMsg { get; set; }
        public string year { get; set; }
        public string remark { get; set; }
        public List<string> data { get; set; }
        public long date { get; set; }
    }
    public class
   AsnUploadResponse
    {
        public bool status { get; set; }
        public string successMsg { get; set; }
        public string errorMsg { get; set; }
        public string year { get; set; }
        public string remark { get; set; }
        public string data { get; set; }
        public long date { get; set; }
    }
    public class Asn
    {
        public string deliveryNo { get; set; }
    };
    public class DataStatusParam
    {
        public List<ModuleidData> Data { get; set; }
    }
    public class ModuleidData
    {
        public string ModuleId { get; set; }
        public string ProductName { get; set; }
    }
    public class DataStatusResponse
    {
        public string Server { get; set; }
        public string ActionName { get; set; }
        public string Result { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
        public string SvrDt0 { get; set; }

        public string SvrDt1 { get; set; }
        public DataStatus Detail { get; set; }
    }
    public class DataStatus
    {
        public List<ModuleidDataStatus> Data { get; set; }
    }
    public class ModuleidDataStatus
    {
        public string ModuleId { get; set; }
        public string ProductName { get; set; }
        public bool Status { get; set; }
        public long SendTime { get; set; }
    }
    public class LinkSnParam
    {
        /// <summary>
        /// 产品二维码
        /// </summary>
        public string snCodes { get; set; }
        /// <summary>
        /// Link二维码
        /// </summary>
        public string LinkSnCodes { get; set; }
    }
    public class SnPackResult
    {
        /// <summary>
        /// 产品二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// Link二维码
        /// </summary>
        public string IS_VALID { get; set; }
    }
    public class ModuleidPackResult
    {
        /// <summary>
        /// 产品二维码
        /// </summary>
        public string Moduleid { get; set; }
        /// <summary>
        /// Link二维码
        /// </summary>
        public string IS_VALID { get; set; }
    }
    public class WO
    {
        /// <summary>
        /// 拦截二维码
        /// </summary>
        public string Wo_Code { get; set; }
        /// <summary>
        /// 显示信息
        /// </summary>
        public string Bb { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Aa { get; set; }
    }
    /// <summary>
    /// TEST_MSR
    /// </summary>
    public class TestMsr
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// ID绑定时间
        /// </summary>
        public DateTime Mdate { get; set; }
    }
    public class TestFpORTData
    {
        public decimal Id { get; set; }
        public string supplierName { get; set; }
        public string serialNumber { get; set; }
        public string line { get; set; }
        public string testStation { get; set; }
        public string factoryName { get; set; }
        public string testType { get; set; }
        public string lotNumber { get; set; }
        public string equipmentNumber { get; set; }
        public string programVersion { get; set; }
        public string partNumber { get; set; }
        public string testTime { get; set; }
        public string barcode { get; set; }
        public string partspec { get; set; }
        public string mac { get; set; }
        public string ip { get; set; }
        public string empid { get; set; }
        public string wo { get; set; }
        public string moduleid { get; set; }
        public string opcode { get; set; }
        public string result { get; set; }
        public string linkmoduleid { get; set; }
        public string deviceid { get; set; }
        public string RouterCode { get; set; }
        public string ngtype { get; set; }
        public int Isvalid { get; set; }
        public string memo1 { get; set; }
        public string memo2 { get; set; }
        public string memo3 { get; set; }
        public string GuId { get; set; }
    }
    public class SnRcardParam
    {
        /// <summary>
        /// 二维码
        /// </summary>
        public string SnCode { get; set; }
        /// <summary>
        /// 流程卡
        /// </summary>
        public string Rcard { get; set; }
    }
    public class SoftClassify 
    {
        /// <summary>
        /// PLM ID
        /// </summary>
        public string CheckNumber { get; set; }
        /// <summary>
        /// MD5码
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// 机种名
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 站位
        /// </summary>
        public string Station { get; set; }
        /// <summary>
        /// 状态-1有效-0无效
        /// </summary>
        public string SwStatus { get; set; }
    }
    public class SoftClassifyView
    {
        /// <summary>
        /// 机种名
        /// </summary>
        public string PART_SPEC { get; set; }
        /// <summary>
        /// MD5码
        /// </summary>
        public string MD5 { get; set; }
        /// <summary>
        /// MAC
        /// </summary>
        public string MAC { get; set; }
    }
    public class ResponseModel
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 其他信息
        /// </summary>
        public dynamic data { get; set; }
    }

    ///<summary>
    ///T_SOFT_CLASSIFY
    ///</summary>
    [Serializable]
    public class SOFTCLASSIFY
    {
        /// <summary>
        ///无参构造方法
        /// </summary>
        public SOFTCLASSIFY() { }

        ///<summary>
        ///cHECK_NUMBER
        ///</summary>
        private string cHECK_NUMBER;
        ///<summary>
        ///cHECK_NUMBER
        ///</summary>
        public string CHECK_NUMBER
        {
            get { return cHECK_NUMBER; }
            set { cHECK_NUMBER = value; }
        }

        ///<summary>
        ///mD5
        ///</summary>
        private string mD5;
        ///<summary>
        ///mD5
        ///</summary>
        public string MD5
        {
            get { return mD5; }
            set { mD5 = value; }
        }

        ///<summary>
        ///pART_SPEC
        ///</summary>
        private string pART_SPEC;
        ///<summary>
        ///pART_SPEC
        ///</summary>
        public string PART_SPEC
        {
            get { return pART_SPEC; }
            set { pART_SPEC = value; }
        }

        ///<summary>
        ///sTATION
        ///</summary>
        private string sTATION;
        ///<summary>
        ///sTATION
        ///</summary>
        public string STATION
        {
            get { return sTATION; }
            set { sTATION = value; }
        }

        ///<summary>
        ///sWSTATUS
        ///</summary>
        private string sWSTATUS;
        ///<summary>
        ///sWSTATUS
        ///</summary>
        public string SWSTATUS
        {
            get { return sWSTATUS; }
            set { sWSTATUS = value; }
        }

        ///<summary>
        ///mDATE
        ///</summary>
        private DateTime mDATE;
        ///<summary>
        ///mDATE
        ///</summary>
        public DateTime MDATE
        {
            get { return mDATE; }
            set { mDATE = value; }
        }

        ///<summary>
        ///uDATE
        ///</summary>
        private DateTime? uDATE;
        ///<summary>
        ///uDATE
        ///</summary>
        public DateTime? UDATE
        {
            get { return uDATE; }
            set { uDATE = value; }
        }
    }


    public class MACSOFTCLASSIFY
    {
        /// <summary>
        /// CHECK_NUMBER
        /// </summary>
        public string CHECKNUMBER { get; set; }

        /// <summary>
        /// MAC
        /// </summary>
        public string MAC { get; set; }

    }
    public class RcardTestData
    {
        /// <summary>
        /// 模组ID
        /// </summary>
        public string Moduleid { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
    }
    public class TestDataDetail
    {
        /// <summary>
        /// 模组ID
        /// </summary>
        public string Moduleid { get; set; }
        /// <summary>
        /// Sn
        /// </summary>
        public string BarCode { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>
        public DateTime TestTime { get; set; }
        /// <summary>
        /// 测试项
        /// </summary>
        public string TestItem { get; set; }
        /// <summary>
        /// 测试项
        /// </summary>
        public string TestSubItem { get; set; }
        /// <summary>
        /// 测试项
        /// </summary>
        public string SubItemTestValue { get; set; }
    }
    public class RcardIO
    {
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string PartSpec { get; set; }
    }
    public class RcardIOParam
    {
        /// <summary>
        /// 流程卡号
        /// </summary>
        public string Rcard { get; set; }
        /// <summary>
        /// 工序
        /// </summary>
        public string OpCode { get; set; }
    }
}
