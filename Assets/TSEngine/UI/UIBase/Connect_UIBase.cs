using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Connect_UIBase : UIBase
	{
		protected Text m_Txt_connecte_Txt;
		protected Image m_Input_Img;
		protected InputField m_Input1_IptField;
		protected Image m_Btn_go_Img;
		protected Button m_Btn_go2_Btn;

		protected ActionManager ActionOnShow = new ActionManager();

		private void Start()
		{
			this.m_Txt_connecte_Txt = this.transform.Find("Image/e_Txt_connecte").GetComponent<Text>();
			this.m_Input_Img = this.transform.Find("Image/e_Input").GetComponent<Image>();
			this.m_Input1_IptField = this.transform.Find("Image/e_Input").GetComponent<InputField>();
			this.m_Btn_go_Img = this.transform.Find("Image/e_Btn_go").GetComponent<Image>();
			this.m_Btn_go2_Btn = this.transform.Find("Image/e_Btn_go").GetComponent<Button>();

			this.AddEventListener();
		}
		public override void OnInit()
		{
			base.OnInit();
		}

		public override void OnShow(System.Object param)
		{
			base.OnShow(param);
		}

		public override void OnHide()
		{
			base.OnHide();
			ActionOnShow.Clear();
		}

		public virtual void OnButtonClicked(GameObject go)
		{

		}
		private void AddEventListener()
		{
			this.m_Input1_IptField.onValueChanged.AddListener(this.Onm_Input1_IptFieldValueChanged);
			this.m_Btn_go2_Btn.onClick.AddListener(this.Onm_Btn_go2_BtnClicked);

		}

		private void Onm_Input1_IptFieldValueChanged(string arg)
		{

		}

		private void Onm_Btn_go2_BtnClicked()
		{
			OnButtonClicked(m_Btn_go2_Btn.gameObject);
		}

	}
}
