using HangFire.Application.Contracts.FaceImageApi.Param;
using HangFire.Common.Base;
using System.Collections;
using System.Threading.Tasks;

namespace HangFire.Application.FaceImageApi
{
    /// <summary>
    /// FaceImage Service Interface
    /// </summary>
    public interface IFaceImageService
    {
        /// <summary>
        /// 执行新增所有员工
        /// </summary>
        /// <returns></returns>
        Task ExcuteInsertAllEmp();

        /// <summary>
        /// 执行新增新员工方法
        /// </summary>
        /// <returns></returns>
        Task<ServiceResult<string>> ExcuteInsertNewEmpAsync();

        /// <summary>
        /// 执行删除离职员工方法
        /// </summary>
        /// <returns></returns>
        Task ExcuteDeleteResignedEmpAsync();

        /// <summary>
        /// 执行更新员工资料的方法
        /// </summary>
        /// <returns></returns>
        Task ExcuteUpdateEmpAsync();


        /// <summary>
        /// Get FaceImage Token
        /// </summary>
        /// <returns></returns>
        Task<string> GetFaceImageToken_Test(string TokenUrl);

        ///// <summary>
        ///// 创建用户并上传图片底库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ServiceResult> CreateUploadUser(CreateUserInput input);

        ///// <summary>
        ///// 接口创建方法
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<string> PostCreateUpLoadUser(CreateUserInput input);

        ///// <summary>
        ///// 接口删除方法
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<string> PostDeleteResignedUser(DeleteUserInput input);

        ///// <summary>
        ///// 根据离职工号获取对应的subjectid集合
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<ArrayList> GetSubListByLeavingEmpNo(SubjectIdInput input);

        ///// <summary>
        ///// 根据当天更新过资料的工号获取对应的subjectid集合和员工实体集合
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task GetUpdatedSubjectid(UpdatedUserInput input);

        ///// <summary>
        ///// Get SubjectId By EmployeeNumber
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //Task<string> GetSubjectIdByEmpNumber(SubjectIdInput input);

    }
}
