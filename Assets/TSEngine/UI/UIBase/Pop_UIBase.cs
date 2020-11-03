using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Pop_UIBase : UIBase
	{
		protected Text m_txt_Pop_Txt;

		private void Start()
		{
			this.m_txt_Pop_Txt = this.transform.Find("WholePanel/e_txt_Pop").GetComponent<Text>();

			this.AddEventListener();
		}
		public override void OnInit()
		{
			base.OnInit();
		}

		public override void OnShow(System.Object Param)
		{
			base.OnShow(Param);
		}

		public override void OnHide()
		{
			base.OnHide();
		}

		public virtual void OnButtonClicked(GameObject go)
		{

		}
		private void AddEventListener()
		{

		}

	}
}
