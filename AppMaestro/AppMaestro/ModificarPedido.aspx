<%@ Title="Modificar Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarPedido.aspx.cs" Inherits="AppMaestro.ModificarPedido" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Modificar pedido</h2>
    </div>

    <div class="row">
       <asp:Label runat="server" Visible="false" ID="lblId"> </asp:Label>
        
        <div class="form-group" >

            <asp:Label runat="server" >Numero Pedido</asp:Label>
            <asp:TextBox ID="txtNumPedido" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>

        </div>

        <div class="form-group">

            <asp:Label runat="server" >Fecha Pedido</asp:Label>
            <asp:TextBox ID="txtFechaPedido" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>

        </div>

         <div class="form-group">

            <asp:Label runat="server" >Cliente</asp:Label>
            <asp:TextBox ID="txtCliente" runat="server" CssClass="form-control" Enabled="false" ></asp:TextBox>

        </div>
         <div class="form-group">

            <asp:Label runat="server" >Telefono</asp:Label>
            <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" ></asp:TextBox>

        </div>
        <div class="form-group">

            <asp:Label runat="server" >Direccion</asp:Label>
            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" ></asp:TextBox>

        </div>

         <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" OnClick="btnActualizar_Click" />
    
    </div>    

</asp:Content>
