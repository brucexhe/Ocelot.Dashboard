using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ocelot.Dashboard.Model
{
    public class Result
    {
        public Result() { }
        public Result(bool _sucess)
        {
            this.Success = _sucess;
        }
        public Result(bool _sucess, string _msg)
        {
            this.Success = _sucess;
            this.Msg = _msg;
        }
        public static Result Sucess()
        {
            return new Result(true, "Operation Success");
        }

        public static Result Fail()
        {
            return new Result(false, "Operation Fail");
        }
        public static Result Fail(string msg)
        {
            return new Result(false, "Operation Fail:" + msg);
        }
        public string Msg { get; set; }
        public bool Success { get; set; }
        public int Status { get; set; }
    }
}
