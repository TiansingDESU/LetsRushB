using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Credits_UIBase : UIBase
	{
		protected Button m_Btn_back_Btn;

		private void Start()
		{
			this.m_Btn_back_Btn = this.transform.Find("Panel/e_Btn_back").GetComponent<Button>();

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
			this.m_Btn_back_Btn.onClick.AddListener(this.Onm_Btn_back_BtnClicked);

		}

		private void Onm_Btn_back_BtnClicked()
		{
			OnButtonClicked(m_Btn_back_Btn.gameObject);
		}

	}
}
