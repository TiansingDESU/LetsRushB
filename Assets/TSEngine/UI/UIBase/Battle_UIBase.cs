using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Battle_UIBase : UIBase
	{
		protected Text m_Txt_blue_Txt;
		protected Text m_Txt_red_Txt;
		protected Image m_Img_end_Img;
		protected Text m_Txt_end_Txt;

		protected ActionManager ActionOnShow = new ActionManager();

		private void Start()
		{
			this.m_Txt_blue_Txt = this.transform.Find("Img_blue/e_Txt_blue").GetComponent<Text>();
			this.m_Txt_red_Txt = this.transform.Find("Img_red/e_Txt_red").GetComponent<Text>();
			this.m_Img_end_Img = this.transform.Find("e_Img_end").GetComponent<Image>();
			this.m_Txt_end_Txt = this.transform.Find("e_Img_end/e_Txt_end").GetComponent<Text>();

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

		}

	}
}