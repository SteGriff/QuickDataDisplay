<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="QuickDataDisplay._Default" %>

<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <table class="collapse ba br2 b--black-10 pv2 ph3">
        <tbody>
            <tr class="striped--light-gray">
                <%foreach (var heading in Results.ColumnHeadings)
                  {
                %>
                <th class="pa2 tl">
                    <%=heading %>
                </th>
                <% } %>
            </tr>
            <% foreach (var line in Results.Lines)
               { %>
            <tr class="striped--light-gray">
                <% foreach (var cell in line)
                   { %>
                <td class="pa2">
                    <%= cell.ToString() %>
                </td>
                <% } %>
            </tr>
            <% } %>
        </tbody>
    </table>
    <p>Refreshed at <%= DateTime.Now %></p>
</asp:Content>
