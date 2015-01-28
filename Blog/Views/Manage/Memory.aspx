<%@ Page Title="" Language="C#" MasterPageFile="~/main.master" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="Leafing.Data.Caching" %>
<%@ Import Namespace="Leafing.Core" %>

<script runat="server">
    protected Process Process = Process.GetCurrentProcess();
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="content">

Mem Usage (Working Set): <%= Process.WorkingSet64.ToString("N") %><br />
VM Size (Private Bytes): <%= Process.PagedMemorySize64.ToString("N") %><br />
GC TotalMemory: <%= GC.GetTotalMemory(false).ToString("N") %><br />
<br />

Current Secends: <%= Util.Secends.ToString("N") %><br />
<br />

Current Time: <%= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %><br />
<br />

Cache Count: <%= CacheProvider.Instance.Count.ToString("N") %><br />
<br />

.Net Framework Version: <%= Environment.Version %><br />

</div>

</asp:Content>
