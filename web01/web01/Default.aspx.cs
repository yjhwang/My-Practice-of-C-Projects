using System;
using System.Web;
using System.Web.UI;

namespace web01
{

    public partial class Default : System.Web.UI.Page
    {
        public void button1Clicked(object sender, EventArgs args)
        {
            button1.Text = "You clicked me";

            Response.Write("<h1>九九乘法表</h1>");
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                    Response.Write("\t" + i * j);
                Response.Write("<br>");
            }
        }
    }
}
