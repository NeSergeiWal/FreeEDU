﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FreeEDU.FreeEDU_Service {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FreeEDU_Service.IFreeEDU_Service")]
    public interface IFreeEDU_Service {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetAccountResponse")]
        string[] GetAccount(string email, string hashPass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetAccountResponse")]
        System.Threading.Tasks.Task<string[]> GetAccountAsync(string email, string hashPass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateAcount", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateAcountResponse")]
        string[] CreateAcount(string login, string Email, string hashPass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateAcount", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateAcountResponse")]
        System.Threading.Tasks.Task<string[]> CreateAcountAsync(string login, string Email, string hashPass);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateCourseResponse")]
        void CreateCourse(string[] course);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateCourseResponse")]
        System.Threading.Tasks.Task CreateCourseAsync(string[] course);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetCoursesResponse")]
        string[][] GetCourses();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetCoursesResponse")]
        System.Threading.Tasks.Task<string[][]> GetCoursesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/AddComletedCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/AddComletedCoursesResponse")]
        void AddComletedCourses(string login, int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/AddComletedCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/AddComletedCoursesResponse")]
        System.Threading.Tasks.Task AddComletedCoursesAsync(string login, int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetComletedCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetComletedCoursesResponse")]
        string[][] GetComletedCourses(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetComletedCourses", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetComletedCoursesResponse")]
        System.Threading.Tasks.Task<string[][]> GetComletedCoursesAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateComment", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateCommentResponse")]
        void CreateComment(string[] comment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/CreateComment", ReplyAction="http://tempuri.org/IFreeEDU_Service/CreateCommentResponse")]
        System.Threading.Tasks.Task CreateCommentAsync(string[] comment);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetComments", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetCommentsResponse")]
        string[][] GetComments(int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetComments", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetCommentsResponse")]
        System.Threading.Tasks.Task<string[][]> GetCommentsAsync(int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/EditAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/EditAccountResponse")]
        bool EditAccount(string login, string image);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/EditAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/EditAccountResponse")]
        System.Threading.Tasks.Task<bool> EditAccountAsync(string login, string image);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/EditCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/EditCourseResponse")]
        void EditCourse(string[] course);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/EditCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/EditCourseResponse")]
        System.Threading.Tasks.Task EditCourseAsync(string[] course);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RemoveAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/RemoveAccountResponse")]
        void RemoveAccount(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RemoveAccount", ReplyAction="http://tempuri.org/IFreeEDU_Service/RemoveAccountResponse")]
        System.Threading.Tasks.Task RemoveAccountAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RemoveCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/RemoveCourseResponse")]
        void RemoveCourse(int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RemoveCourse", ReplyAction="http://tempuri.org/IFreeEDU_Service/RemoveCourseResponse")]
        System.Threading.Tasks.Task RemoveCourseAsync(int courseId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetEmails", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetEmailsResponse")]
        string[] GetEmails();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetEmails", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetEmailsResponse")]
        System.Threading.Tasks.Task<string[]> GetEmailsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/SendRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/SendRequestResponse")]
        void SendRequest(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/SendRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/SendRequestResponse")]
        System.Threading.Tasks.Task SendRequestAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/AcceptRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/AcceptRequestResponse")]
        void AcceptRequest(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/AcceptRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/AcceptRequestResponse")]
        System.Threading.Tasks.Task AcceptRequestAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RejectRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/RejectRequestResponse")]
        void RejectRequest(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/RejectRequest", ReplyAction="http://tempuri.org/IFreeEDU_Service/RejectRequestResponse")]
        System.Threading.Tasks.Task RejectRequestAsync(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetRequests", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetRequestsResponse")]
        string[][] GetRequests();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetRequests", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetRequestsResponse")]
        System.Threading.Tasks.Task<string[][]> GetRequestsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/ChangeRole", ReplyAction="http://tempuri.org/IFreeEDU_Service/ChangeRoleResponse")]
        void ChangeRole(string[] account);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/ChangeRole", ReplyAction="http://tempuri.org/IFreeEDU_Service/ChangeRoleResponse")]
        System.Threading.Tasks.Task ChangeRoleAsync(string[] account);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetAccounts", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetAccountsResponse")]
        string[][] GetAccounts();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFreeEDU_Service/GetAccounts", ReplyAction="http://tempuri.org/IFreeEDU_Service/GetAccountsResponse")]
        System.Threading.Tasks.Task<string[][]> GetAccountsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFreeEDU_ServiceChannel : FreeEDU.FreeEDU_Service.IFreeEDU_Service, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FreeEDU_ServiceClient : System.ServiceModel.ClientBase<FreeEDU.FreeEDU_Service.IFreeEDU_Service>, FreeEDU.FreeEDU_Service.IFreeEDU_Service {
        
        public FreeEDU_ServiceClient() {
        }
        
        public FreeEDU_ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FreeEDU_ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FreeEDU_ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FreeEDU_ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] GetAccount(string email, string hashPass) {
            return base.Channel.GetAccount(email, hashPass);
        }
        
        public System.Threading.Tasks.Task<string[]> GetAccountAsync(string email, string hashPass) {
            return base.Channel.GetAccountAsync(email, hashPass);
        }
        
        public string[] CreateAcount(string login, string Email, string hashPass) {
            return base.Channel.CreateAcount(login, Email, hashPass);
        }
        
        public System.Threading.Tasks.Task<string[]> CreateAcountAsync(string login, string Email, string hashPass) {
            return base.Channel.CreateAcountAsync(login, Email, hashPass);
        }
        
        public void CreateCourse(string[] course) {
            base.Channel.CreateCourse(course);
        }
        
        public System.Threading.Tasks.Task CreateCourseAsync(string[] course) {
            return base.Channel.CreateCourseAsync(course);
        }
        
        public string[][] GetCourses() {
            return base.Channel.GetCourses();
        }
        
        public System.Threading.Tasks.Task<string[][]> GetCoursesAsync() {
            return base.Channel.GetCoursesAsync();
        }
        
        public void AddComletedCourses(string login, int courseId) {
            base.Channel.AddComletedCourses(login, courseId);
        }
        
        public System.Threading.Tasks.Task AddComletedCoursesAsync(string login, int courseId) {
            return base.Channel.AddComletedCoursesAsync(login, courseId);
        }
        
        public string[][] GetComletedCourses(string login) {
            return base.Channel.GetComletedCourses(login);
        }
        
        public System.Threading.Tasks.Task<string[][]> GetComletedCoursesAsync(string login) {
            return base.Channel.GetComletedCoursesAsync(login);
        }
        
        public void CreateComment(string[] comment) {
            base.Channel.CreateComment(comment);
        }
        
        public System.Threading.Tasks.Task CreateCommentAsync(string[] comment) {
            return base.Channel.CreateCommentAsync(comment);
        }
        
        public string[][] GetComments(int courseId) {
            return base.Channel.GetComments(courseId);
        }
        
        public System.Threading.Tasks.Task<string[][]> GetCommentsAsync(int courseId) {
            return base.Channel.GetCommentsAsync(courseId);
        }
        
        public bool EditAccount(string login, string image) {
            return base.Channel.EditAccount(login, image);
        }
        
        public System.Threading.Tasks.Task<bool> EditAccountAsync(string login, string image) {
            return base.Channel.EditAccountAsync(login, image);
        }
        
        public void EditCourse(string[] course) {
            base.Channel.EditCourse(course);
        }
        
        public System.Threading.Tasks.Task EditCourseAsync(string[] course) {
            return base.Channel.EditCourseAsync(course);
        }
        
        public void RemoveAccount(string login) {
            base.Channel.RemoveAccount(login);
        }
        
        public System.Threading.Tasks.Task RemoveAccountAsync(string login) {
            return base.Channel.RemoveAccountAsync(login);
        }
        
        public void RemoveCourse(int courseId) {
            base.Channel.RemoveCourse(courseId);
        }
        
        public System.Threading.Tasks.Task RemoveCourseAsync(int courseId) {
            return base.Channel.RemoveCourseAsync(courseId);
        }
        
        public string[] GetEmails() {
            return base.Channel.GetEmails();
        }
        
        public System.Threading.Tasks.Task<string[]> GetEmailsAsync() {
            return base.Channel.GetEmailsAsync();
        }
        
        public void SendRequest(string login) {
            base.Channel.SendRequest(login);
        }
        
        public System.Threading.Tasks.Task SendRequestAsync(string login) {
            return base.Channel.SendRequestAsync(login);
        }
        
        public void AcceptRequest(string login) {
            base.Channel.AcceptRequest(login);
        }
        
        public System.Threading.Tasks.Task AcceptRequestAsync(string login) {
            return base.Channel.AcceptRequestAsync(login);
        }
        
        public void RejectRequest(string login) {
            base.Channel.RejectRequest(login);
        }
        
        public System.Threading.Tasks.Task RejectRequestAsync(string login) {
            return base.Channel.RejectRequestAsync(login);
        }
        
        public string[][] GetRequests() {
            return base.Channel.GetRequests();
        }
        
        public System.Threading.Tasks.Task<string[][]> GetRequestsAsync() {
            return base.Channel.GetRequestsAsync();
        }
        
        public void ChangeRole(string[] account) {
            base.Channel.ChangeRole(account);
        }
        
        public System.Threading.Tasks.Task ChangeRoleAsync(string[] account) {
            return base.Channel.ChangeRoleAsync(account);
        }
        
        public string[][] GetAccounts() {
            return base.Channel.GetAccounts();
        }
        
        public System.Threading.Tasks.Task<string[][]> GetAccountsAsync() {
            return base.Channel.GetAccountsAsync();
        }
    }
}
