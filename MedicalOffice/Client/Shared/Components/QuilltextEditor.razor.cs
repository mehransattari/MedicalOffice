using Blazored.TextEditor;
using Microsoft.AspNetCore.Components;

namespace MedicalOffice.Client.Shared.Components;

public class QuilltextEditorBase : ComponentBase
{
    [Parameter]
    public Action<string> OnValueChanged { get; set; }

    [Parameter]
    public string ReceiveText { get; set; } = string.Empty;


    public BlazoredTextEditor QuillHtml = new BlazoredTextEditor();

    public string toolbar = """"...markup here..."""";
    public string editorContent = "";

    protected override void OnInitialized()
    {
        toolbar = """"
                  <span class="ql-formats">
                <select class="ql-font">
                    <option selected=""></option>
                    <option value="serif"></option>
                    <option value="monospace"></option>
                </select>
                <select class="ql-size">
                    <option value="small"></option>
                    <option selected=""></option>
                    <option value="large"></option>
                    <option value="huge"></option>
                </select>
            </span>
            <span class="ql-formats">
                <button class="ql-bold"></button>
                <button class="ql-italic"></button>
                <button class="ql-underline"></button>
                <button class="ql-strike"></button>
            </span>
            <span class="ql-formats">
                <select class="ql-color"></select>
                <select class="ql-background"></select>
            </span>
            <span class="ql-formats">
                <button class="ql-list" value="ordered"></button>
                <button class="ql-list" value="bullet"></button>
                <button class="ql-indent" value="-1"></button>
                <button class="ql-indent" value="+1"></button>
                <select class="ql-align">
                    <option selected=""></option>
                    <option value="center"></option>
                    <option value="right"></option>
                    <option value="justify"></option>
                </select>
            </span>
            <span class="ql-formats">
                <button class="ql-link"></button>
            </span>
            <span class="ql-formats">
                <button class="ql-image"></button>
            </span>



           
            """";
        editorContent = ReceiveText;
    }
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        editorContent = ReceiveText;
        var htmlValue = await QuillHtml.GetHTML();
        OnValueChanged?.Invoke(htmlValue);
    }

    public async Task ChangeText()
    {
        var htmlValue = await QuillHtml.GetHTML();
        OnValueChanged?.Invoke(htmlValue);
    }



}
