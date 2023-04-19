<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Apresentacao._Default" %>

<asp:Content ID="idContent" ContentPlaceHolderID="MainContent" runat="server">
     <script type="text/javascript">
         $(document).ready(function () {
             $('#<%=txtCEP.ClientID %>').mask('99999-999');

            <%-- $('#<%=btnSearchCEP.ClientID %>').click(function (event) {
                 if ($('#MainContent_txtCEP').val() != '') {
                     $('#divComplement').show();
                     event.preventDefault();
                 }
             });--%>
            
         });

       
     </script>
<div class="row">
        <div class="col-md-12">
             <div class="col-md-12">
                      <h2>Consulte CEP</h2> 
                 </div>
            <div class="col-md-12">
           <div class="col-md-2"><asp:TextBox ID="txtCEP" runat="server"></asp:TextBox></div>            
            <div class="col-md-4"><asp:Button ID="btnSearchCEP" runat="server"  Text="Consultar CEP" OnClick="btnSearchCEP_Click"  /></div>
                </div>
        </div>  
      <div class="col-md-12">
                 <div class="col-md-12">
                     <h2>Consultar no banco CEP por UF</h2>
                 </div>
                <div class="col-md-12">
                    <div class="col-md-2"><asp:DropDownList ID="ddlUf" style="width: 6vw;height: 1.5vw;" runat="server"></asp:DropDownList></div>
                    <div class="col-md-4"><asp:Button ID="btnSearchUF" runat="server"  Text="Consultar UF" OnClick="btnSearchUF_Click"  /></div>
                </div>
            </div>
      <div class="col-md-12" style="margin-top:1.5vw;">
               <div class="col-md-4"><asp:Label style="color:red" Visible="false" ID="msgReturn" runat="server"></asp:Label></div>
            </div>
        <asp:Panel class="col-md-12" ID="divComplement" Visible="false" runat="server" style="margin-top:1.5vw;">
            <div class="col-md-12">
                <div class="col-md-2">
                    <span>CEP:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtCEPResp" disabled></asp:TextBox>
                </div>
            </div>
             <div class="col-md-12">
                <div class="col-md-2">
                    <span>Logradouro:</span>

                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtLogradouro" disabled></asp:TextBox>
                </div>
            </div>
             <div class="col-md-12">
                <div class="col-md-2">
                    <span>Complemento:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtComplemento" disabled></asp:TextBox>
                </div>
            </div>
              <div class="col-md-12">
                <div class="col-md-2">
                    <span>Bairro:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtBairro" disabled></asp:TextBox>
                </div>
            </div>
              <div class="col-md-12">
                <div class="col-md-2">
                    <span>Localidade:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtLocalidade" disabled></asp:TextBox>
                </div>
            </div>
             <div class="col-md-12">
                <div class="col-md-2">
                    <span>UF:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtUF" disabled></asp:TextBox>
                </div>
            </div>
              <div class="col-md-12">
                <div class="col-md-2">
                    <span>Unidade:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtUnidade" disabled></asp:TextBox>
                </div>
            </div>
             <div class="col-md-12">
                <div class="col-md-2">
                    <span>IBGE:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtIbge" disabled></asp:TextBox>
                </div>
            </div>
             <div class="col-md-12">
                <div class="col-md-2">
                    <span>GIA:</span>
                </div>
                <div class="col-md-8">
                    <asp:TextBox style="width:100%" runat="server" ID="txtGia" disabled></asp:TextBox>
                </div>
            </div>
        </asp:Panel>
    <asp:Panel class="col-md-12" ID="divGrid" Visible="false" runat="server" style="margin-top:1.5vw;">
        <asp:GridView runat="server" ID="gridViewCEP"></asp:GridView>

    </asp:Panel>
    </div>
   
</asp:Content>

