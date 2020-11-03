/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class UICodeConfig : MonoBehaviour
{
    /// <summary>
    /// 组件的列表
    /// </summary>
    public static string[] uiComponentName = new string[] { "Transform", "Text", "Image", "RawImage", "Button", "Toggle", "Slider", "Scrollbar", "InputField", "ScrollRect", "Dropdown", "Mask" };
    /// <summary>
    /// 组件的名字后缀
    /// </summary> 
    public static string[] uiVariableName = new string[] { "Trans", "Txt", "Img", "RImg", "Btn", "Tgl", "Slider", "Scrlbar", "IptField", "ScrollRect", "Dropdown", "Mask" };


    #region C#代码配置
    public const string variable = "\t\tprotected {0} {1}; \n";

    public const string namespaceStr = "using System.Collections; \nusing System.Collections.Generic; \nusing UnityEngine; \nusing UnityEngine.UI; \n";

    public const string namespaceRegionStart = "\nnamespace Assets\n{\n";

    public const string className = "\n\tpublic class {0}Base : UIBase\n\t{{\n";

    public const string ActionMgrStr = "\n\t\tprotected ActionManager ActionOnShow = new ActionManager();\n";

    public const string startName = "\n\t\tprivate void Start()\n\t\t{ \n";

    public const string updateName = "\n\t\tprivate void Update()\n\t\t{\n\n\t\t}\n"; 

    public const string ShowName = "\n\t\tpublic override void OnShow(System.Object param)\n\t\t{\n\t\t\tbase.OnShow(param);\n\t\t}\n";

    public const string HideName = "\n\t\tpublic override void OnHide()\n\t\t{\n\t\t\tbase.OnHide();\n\t\t\tActionOnShow.Clear();\n\t\t}\n";

    public const string InitName = "\n\t\tpublic override void OnInit()\n\t\t{\n\t\t\tbase.OnInit();\n\t\t}\n";

    public const string OnAnyBtnClickName = "\n\t\tpublic virtual void OnButtonClicked(GameObject go)\n\t\t{\n\n\t\t}\n";

    public const string onBtnClickName = "\n\t\tprivate void On{0}Clicked()\n\t\t{{\n\t\t\tOnButtonClicked({1}.gameObject);\n\t\t}}\n";

    public const string onValueChangedName = "\n\t\tprivate void On{0}ValueChanged({1} arg)\n\t\t{{\n\n\t\t}}\n";

    public const string methodEnd = "\n\t\t}\n";

    public const string btnListenerName = "\t\t\tthis.{0}.onClick.AddListener(this.On{1}Clicked);\n";

    public const string valueChangedName = "\t\t\tthis.{0}.onValueChanged.AddListener(this.On{1}ValueChanged);\n";

    public const string addEventName = "\t\tprivate void AddEventListener()\n\t\t{\n";

    public const string end = "\n\t\t}";

    public const string classEnd = "\n\t}";

    public const string uiPath = "\t\t\tthis.{0} = this.transform.Find(\"{1}\").GetComponent<{2}>();\n";

    public const string addEventMethod = "\n\t\t\tthis.AddEventListener();";

    public const string namespaceRegionEnd = "\n}";
    #endregion

    #region Lua代码配置
    public const string luaModule = "module(\"{0}\", package.seeall) \n\n;";

    public const string luaClassName = "\n\tfunction {0}:InitUI()";

    public const string luaAddEvent = "\n\tfunction {0}:AddEvent() \n";

    public const string luaClickEvent = "\n\tfunction {0}:On{1}Clicked()\n\n\n\tend\n";

    public const string luaValueChangedEvent = "\n\tfunction {0}:On{1}ValueChanged(arg)\n\n\n\tend\n";

    public const string luaVariable = "\n\t\tself.{0} = self.gameObject.transform:Find(\"{1}\"):GetComponent(\"{2}\");";

    public const string luaEventAdd = "\n\t\tself.{0}.onClick:AddListener(function() self:On{1}Clicked(); end)";

    public const string luaEventChanged = "\n\t\tself.{0}.onValueChanged:AddListener(function(args) self:On{1}ValueChanged(args); end)";

    public const string luaEnd = "\n\tend\n";

    public const string luaAddEventMethod = "\n\n\t\tself:AddEvent();";
    #endregion
}

/*
author： Lai Zhang Yin，
description ： If you have any question or suggestion, please add QQ/Wechat : 782966734
*/
