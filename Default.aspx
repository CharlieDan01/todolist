<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="todolist._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
        <section class="row" aria-labelledby="aspnetTitle">
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            <h1 id="aspnetTitle">TO DO LIST</h1>
            <p class="lead">Welcome to this aplication made with C#, ASP.NET and MySQL</p>
            
        </section>

        <div class="row">
            <section class="col-md-3" aria-labelledby="gettingStartedTitle">
                 <asp:Label ID="Label5" runat="server" Text="ID"></asp:Label>
                 <asp:TextBox ID="txtID" runat="server" CssClass="form-control" Enabled="false" placeholder="Only Read"></asp:TextBox>

                <asp:Label ID="Label1" runat="server" Text="Tittle" ></asp:Label>

                <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" MaxLength="30" placeHolder="Insert Tittle"></asp:TextBox>

                <asp:Label ID="Label2" runat="server" Text="Details"></asp:Label>

                <asp:TextBox ID="txtDet" runat="server" CssClass="form-control" MaxLength="999" placeholder="Insert Details"></asp:TextBox>

                <asp:Label ID="Label3" runat="server" Text="Priority"></asp:Label>

                <asp:DropDownList ID="ddlPrio" runat="server" CssClass="form-control">
                    <asp:ListItem Text="--SELECT--" Value="-"></asp:ListItem>
                    <asp:ListItem Text="HIGH" Value="A"></asp:ListItem>
                    <asp:ListItem Text="MEDIUM" Value="B"></asp:ListItem>
                    <asp:ListItem Text="LOW" Value="C"></asp:ListItem>
                </asp:DropDownList>
               <asp:Label ID="Label4" runat="server" Text="Done?"></asp:Label>
                <br />
                <asp:CheckBox ID="checkDo" runat="server" CssClass="" Enabled="false"/>
                <asp:Label ID="lblCheckError" runat="server" CssClass="text-danger small-text" Text="* A new task cannot be marked as completed." Visible="true"></asp:Label>

                <br />
                <asp:Label ID="Label6" runat="server" Text="Select the due date to save the task"></asp:Label>
                <asp:Calendar ID="Calendar1" runat="server" HeaderText="Fecha límite" DataFormatString="{0:yyyy-MM-dd}"></asp:Calendar>
                <br />
                <div class="row">
                    <div class="col-md-4">
                        <asp:Button ID="btnAdd" runat="server" Text="ADD" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnClear" runat="server" Text="CLEAR" CssClass="btn btn-secondary" OnClick="btnClear_Click" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnEdit" runat="server" Text="EDIT" CssClass="btn btn-warning" OnClick="btnEdit_Click" />
                    </div>
                    
                </div>
                
            </section>
            <section class="col-md-9" aria-labelledby="librariesTitle">
                <asp:GridView ID="gridListTD" runat="server" CssClass="table table-hover table-striped" OnRowCommand="gridListTD_RowCommand" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="ID" />
                        <asp:BoundField DataField="title" HeaderText="Title" />
                        <asp:BoundField DataField="details" HeaderText="Details" />
                        <asp:BoundField DataField="priority" HeaderText="Priority" />
                        <asp:CheckBoxField DataField="isCompleted" HeaderText="Completed" />
                        <asp:BoundField DataField="maxDate" HeaderText="MaxDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false"/>

                       <asp:TemplateField HeaderText="Actions">
                            <ItemTemplate>
                                <asp:Button ID="btnSelect" 
                                            runat="server" 
                                            Text="Select" 
                                            CommandName="SelectRow" 
                                            CommandArgument="<%# Container.DataItemIndex %>" 
                                            CssClass="btn btn-primary btn-sm" />

                                <asp:Button ID="btnDelete" 
                                            runat="server" 
                                            Text="Delete" 
                                            CommandName="DeleteRow" 
                                            CommandArgument='<%# Eval("Id") %>' 
                                            CssClass="btn btn-danger btn-sm" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </section>
           
        </div>
    </main>

</asp:Content>
