using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            #region
            //int[] comparValue = { 0, 0, 11, 11, 22, 11, 11, 22, 22 };

            ////方法1
            //var tempV = from c in comparValue group c by c into g select new { value = g.Key, count = g.Count() };
            //var v1 = (from v in tempV orderby v.count descending select v.value).First();
            //Console.WriteLine(v1.ToString());

            ////方法2
            //var query = (from num in
            //                 (
            //                 from number in comparValue
            //                 group number by number into g
            //                 select new
            //                 {
            //                     number = g.Key,
            //                     cnt = g.Count()
            //                 }
            //             )
            //             orderby num.cnt descending
            //             select num.number).First();
            //Console.WriteLine("{0}", query);

            ////方法3
            //var res = from n in comparValue
            //          group n by n into g
            //          orderby g.Count() descending
            //          select g;
            //// 分组中第一个组就是重复最多的
            //var gr = res.First();
            //Console.WriteLine(gr.Key);
            #endregion

            //1、内连接(join)查询
            //Lambda写法
            var data = Emp.Join(Dept, e => e.DeptId, d => d.DeptId, (e, d) => new { e, d }).ToList();
            //Linq写法
            var data2 = (from e in Emp
                         join d in Dept
                         on e.DeptId equals d.DeptId
                         select new { e, d }).ToList();

            //2、左(left join )连接查询
            //Linq写法
            var data3 = (from e in Emp
                         join d in Dept
                         on e.DeptId equals d.DeptId into list
                         from dept in list
                         select new { e, dept }).ToList();
            //Lambda写法
            var data4 = Emp.GroupJoin(Dept, e => e.DeptId, d => d.DeptId, (e, d) => new { e, d = d.FirstOrDefault() }).ToList();

            //3、交叉(Corss join)连接
            //Linq写法
            var data5 = (from e in Emp
                         from d in Dept
                         select new { e, d }).ToList();
            //Lambda写法
            var data6 = Emp.SelectMany(emp => Dept.Select(dept => new { emp, dept })).ToList();


            Console.ReadKey();
        }
        /// <summary>
        /// 员工数据
        /// </summary>
        private static List<Employee> Emp = new List<Employee>()
        {
            new Employee {Empid=1,DeptId=6,EmpName="王芳",EmpCode="1001" },
            new Employee {Empid=2,DeptId=5,EmpName="韩丽",EmpCode="1002" },
            new Employee {Empid=3,DeptId=3,EmpName="李飞",EmpCode="1003" },
            new Employee {Empid=4,DeptId=2,EmpName="李丽",EmpCode="1004" },
            new Employee {Empid=5,DeptId=4,EmpName="王二麻",EmpCode="1005" },
            new Employee {Empid=6,DeptId=1,EmpName="刘慧",EmpCode="1006" },
            new Employee {Empid=7,DeptId=1,EmpName="张飞",EmpCode="1007" },
            new Employee {Empid=8,DeptId=8,EmpName="测试人员",EmpCode="1008" },
        };
        /// <summary>
        /// 部门数据
        /// </summary>
        private static List<DeptModel> Dept = new List<DeptModel>()
        {
            new DeptModel {DeptId=1,DeptName="产品部" },
            new DeptModel {DeptId=2,DeptName="管理层" },
            new DeptModel {DeptId=3,DeptName="人事部" },
            new DeptModel {DeptId=4,DeptName="研发部" },
            new DeptModel {DeptId=5,DeptName="项目部" },
            new DeptModel {DeptId=6,DeptName="市场部" },
            new DeptModel {DeptId=7,DeptName="测试部门" },
        };
    }


    /// <summary>
    /// 员工类
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// 员工id
        /// </summary>
        public int Empid { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmpName { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmpCode { get; set; }
    }
    /// <summary>
    /// 部门模型
    /// </summary>
    public class DeptModel
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public int DeptId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DeptName { get; set; }
    }
}
