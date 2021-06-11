<%@  Title="Consultar Detalle" Language="C#" MasterPageFile="~/Site.Master" enableEventValidation="true" AutoEventWireup="true" CodeBehind="ConsultarDetalle.aspx.cs" Inherits="AppMaestro.ConsultarDetalle" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
    <script language="javascript" type="text/javascript">

        function mensaje() {
            swal("Desea eliminar el producto?", {
                buttons: {
                    cancel: "cancelar",
                    catch: {
                        text: "Aceptar",
                        value: "Ok",
                    },
                },
            })
                .then((value) => {
                    switch (value) {
                        case "Ok":
                            $.ajax({
                                type: "POST",
                                url: "ConsultarDetalle.aspx/eliminarproducto",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                data: {Id:1},

                                success: function (data) {
                                    alert("Informacion actualizada")
                                },
                                error: function (data) {
                                    alert("Error")
                                }
                            });
                            break;

                    }
                });

        }
    </script>

    <div class="jumbotron">
        <h2>Pedidos</h2>
    </div>

    <div class="row">
        <asp:Label runat="server" ID="lblId" Visible="false"> </asp:Label>

        <asp:GridView ID="gvpedidos" runat="server" AutoGenerateColumns="false" Visible ="false" CssClass="table table-striped" 
            OnRowCommand="gvpedidos_RowCommand" DataKeyNames="IdDetalle">    
             <Columns>    
                 <asp:BoundField DataField="IdDetalle" HeaderText="Item ID" ItemStyle-Width="30"/>    
                 <asp:BoundField DataField="Producto" HeaderText="Producto" ItemStyle-Width="30" />    
                 <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" ItemStyle-Width="150" />    
                 <asp:BoundField DataField="Precio" HeaderText="Precio" ItemStyle-Width="150" />   
                 <asp:BoundField DataField="SubTotal" HeaderText="SubTotal" ItemStyle-Width="150" />  
                 <asp:TemplateField>
                  <ItemTemplate>
                    <asp:Button ID="btnEliminar" runat="server"
                      CommandName="Eliminar"
                      CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                      Text="Eliminar" />                   
                  </ItemTemplate>
                </asp:TemplateField>
             </Columns>    
         </asp:GridView>   
    </div>    
</asp:Content>