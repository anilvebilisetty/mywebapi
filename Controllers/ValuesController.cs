using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Cors;

namespace GovtWalletApi.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        [Route("GetPlanNames")]
        public ActionResult<IEnumerable<string>> GetPlanNames()
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("select distinct PlanName from Plans", con);
            command.CommandType = System.Data.CommandType.Text;
            con.Open();
            SqlDataReader adp = command.ExecuteReader();
            List<String> result = new List<String>();
            while (adp.Read())
            {
                string PlanName;
              PlanName = adp["PlanName"].ToString();
                
                result.Add(PlanName);
            }
            string str1 = "new brach cahnges";
            return result;

        }

        [HttpGet]
        [Route("GetPlanDetails")]
        public ActionResult<IEnumerable<Plan>> GetPlanDetails()
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("select * from Plans", con);
            command.CommandType = System.Data.CommandType.Text;
            con.Open();
            SqlDataReader adp = command.ExecuteReader();
            List<Plan> result = new List<Plan>();
            while (adp.Read())
            {
                Plan plan = new Plan();
                plan.PlanName = adp["PlanName"].ToString();
                plan.BudgetAllocated = int.Parse(adp["BudgetAllocated"].ToString());
                plan.RemainingBudget = int.Parse(adp["RemainingBudget"].ToString());
                result.Add(plan);
            }
            string str1 = "new brach cahnges";
            return result;

        }


        [HttpGet]
        [Route("GetExpensesDetails")]
        public ActionResult<IEnumerable<Expenses>> GetExpensesDetails(string plan)
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("select * from expenses where planname = '"+plan+"'", con);
            command.CommandType = System.Data.CommandType.Text;
            con.Open();
            SqlDataReader adp = command.ExecuteReader();
            List<Expenses> result = new List<Expenses>();
            while (adp.Read())
            {
                Expenses exp = new Expenses();
                exp.Aadhar = adp["aadharnumber"].ToString();
                exp.FirstName = adp["firstname"].ToString();
                exp.lastName = adp["lastname"].ToString();
                exp.Address = adp["address"].ToString();
                exp.City = adp["city"].ToString();
                exp.State = adp["state"].ToString();
                exp.AmountCredited = int.Parse(adp["amountcredited"].ToString());
                result.Add(exp);
            }
            return result;
    }




        // GET api/values/5
        [HttpGet("{id}")]
        [Route("GetPlanRemainigBudget")]
        public ActionResult<int> Get(string planname)
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("select RemainingBudget from Plans where PlanName='"+planname+"'", con);
            command.CommandType = System.Data.CommandType.Text;
            con.Open();
            int remainingbudget = (int)command.ExecuteScalar();
            return remainingbudget;
        }

      

        [HttpPost]
        [Route("InsertPlanDetails")]
        public void PostPlanDetails([FromBody] Plan value)
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("spInsertPlan", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PlanName", value.PlanName);
            command.Parameters.AddWithValue("@BudgetAllocated", value.BudgetAllocated);
            con.Open();
           int rowseffected = command.ExecuteNonQuery();
        
        }

        [HttpPost]
        [Route("InsertExpenses")]
        public void PostExpensesDetails([FromBody] Expenses value)
        {
            SqlConnection con = new SqlConnection(@"data source=LENOVO-PC\SQLEXPRESS;initial catalog=Wallet;integrated security=True");
            SqlCommand command = new SqlCommand("spInsertExpenses", con);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@aadhar", value.Aadhar);
            command.Parameters.AddWithValue("@firstname", value.FirstName);
            command.Parameters.AddWithValue("@lastname", value.lastName);
            command.Parameters.AddWithValue("@address", value.Address);
            command.Parameters.AddWithValue("@city", value.City);
            command.Parameters.AddWithValue("@state", value.State);
            command.Parameters.AddWithValue("@planname", value.PlanName);
            command.Parameters.AddWithValue("@amountcredited", value.AmountCredited);
            con.Open();
            int rowseffected = command.ExecuteNonQuery();

        }

    }
}
