<%@ Page Title="Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AppMaestro._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Pedidos</h2>
    </div>

    <div class="row">
        <asp:Button runat="server" id="btnObtener" Text="Obtener pedidos" OnClick="btnObtener_Click" />


        <asp:GridView ID="gvpedidos" runat="server" AutoGenerateColumns="false" Visible ="false" CssClass="table table-striped" 
            OnRowCommand="gvpedidos_RowCommand" DataKeyNames="IdPedido">    
             <Columns>    
                 <asp:BoundField DataField="IdPedido" HeaderText="Item ID" ItemStyle-Width="30" />    
                 <asp:BoundField DataField="NumPedido" HeaderText="Numero Pedido" ItemStyle-Width="30" />    
                 <asp:BoundField DataField="FechaPedido" HeaderText="Fecha Pedido" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Cliente" HeaderText="Cliente" ItemStyle-Width="150" />   
                 <asp:BoundField DataField="Telefono" HeaderText="Telefono" ItemStyle-Width="150" />  
                 <asp:BoundField DataField="Direccion" HeaderText="Direccion" ItemStyle-Width="150" />
                 <asp:TemplateField>
                  <ItemTemplate>
                    <asp:Button ID="btnModificar" runat="server"
                      CommandName="Modificar"
                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                      Text="Modificar" />
                    <asp:Button ID="btnConsultar" runat="server"
                      CommandName="Consultar"
                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                      Text="Consultar" />
                  </ItemTemplate>
                </asp:TemplateField>
             </Columns>    
         </asp:GridView>   
    </div>    

</asp:Content>
