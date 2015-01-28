<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>
<%@ Import Namespace="System.Data" %>

<script runat="server">
    public DataSet ds;
    public string sql;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if(ds != null && ds.Tables.Count >= 1)
        {
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
    }
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">
    <form action="<%= UrlTo<ManageController>(p => p.RunSql()) %>" method="post" class="edit_content">
        <label for="sql">内容：</label><br /><textarea id="sql" name="sql" cols="100" rows="20"><%= sql %></textarea>
        <br /><br />
        <input name="commit" type="submit" value="提交" />
        
    </form>
    <form runat="server" id="form2">
        <asp:GridView ID="GridView1" runat="server" AllowPaging="true" PageSize="20">
        </asp:GridView>
    </form>
    <p style="color: Green"><%= Flash.Notice %></p>
    <p style="color: Red"><%= Flash.Warning %></p>
</div>

</asp:Content>

