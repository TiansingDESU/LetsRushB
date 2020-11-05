using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Main_UIBase : UIBase
	{
		protected Transform m_grid_Trans;
		protected Button m_Btn_new_Btn;
		protected Button m_Btn_load_Btn;
		protected Button m_Btn_exit_Btn;
		protected Button m_Btn_test_Btn;

		protected ActionManager ActionOnShow = new ActionManager();

		private void Start()
		{
			this.m_grid_Trans = this.transform.Find("LeftPanel/ScrollView/Viewport/e_grid").GetComponent<Transform>();
			this.m_Btn_new_Btn = this.transform.Find("RightPanel/e_Btn_new").GetComponent<Button>();
			this.m_Btn_load_Btn = this.transform.Find("RightPanel/e_Btn_load").GetComponent<Button>();
			this.m_Btn_exit_Btn = this.transform.Find("RightPanel/e_Btn_exit").GetComponent<Button>();
			this.m_Btn_test_Btn = this.transform.Find("RightPanel/e_Btn_test").GetComponent<Button>();

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
			this.m_Btn_new_Btn.onClick.AddListener(this.Onm_Btn_new_BtnClicked);
			this.m_Btn_load_Btn.onClick.AddListener(this.Onm_Btn_load_BtnClicked);
			this.m_Btn_exit_Btn.onClick.AddListener(this.Onm_Btn_exit_BtnClicked);
			this.m_Btn_test_Btn.onClick.AddListener(this.Onm_Btn_test_BtnClicked);

		}

		private void Onm_Btn_new_BtnClicked()
		{
			OnButtonClicked(m_Btn_new_Btn.gameObject);
		}

		private void Onm_Btn_load_BtnClicked()
		{
			OnButtonClicked(m_Btn_load_Btn.gameObject);
		}

		private void Onm_Btn_exit_BtnClicked()
		{
			OnButtonClicked(m_Btn_exit_Btn.gameObject);
		}

		private void Onm_Btn_test_BtnClicked()
		{
			OnButtonClicked(m_Btn_test_Btn.gameObject);
		}

	}
}