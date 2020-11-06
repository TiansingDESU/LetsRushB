using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Room_UIBase : UIBase
	{
		protected Transform m_grid_Trans;
		protected Transform m_grid1_Trans;
		protected Transform m_grid2_Trans;
		protected Image m_Btn_ready_Img;
		protected Button m_Btn_ready3_Btn;
		protected Text m_Txt_ready_Txt;
		protected Image m_Btn_start_Img;
		protected Button m_Btn_start4_Btn;
		protected Image m_Btn_leave_Img;
		protected Button m_Btn_leave5_Btn;

		protected ActionManager ActionOnShow = new ActionManager();

		private void Start()
		{
			this.m_grid_Trans = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid").GetComponent<Transform>();
			this.m_grid1_Trans = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid").GetComponent<Transform>();
			this.m_grid2_Trans = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid").GetComponent<Transform>();
			this.m_Btn_ready_Img = this.transform.Find("Image/e_Btn_ready").GetComponent<Image>();
			this.m_Btn_ready3_Btn = this.transform.Find("Image/e_Btn_ready").GetComponent<Button>();
			this.m_Txt_ready_Txt = this.transform.Find("Image/e_Btn_ready/e_Txt_ready").GetComponent<Text>();
			this.m_Btn_start_Img = this.transform.Find("Image/e_Btn_start").GetComponent<Image>();
			this.m_Btn_start4_Btn = this.transform.Find("Image/e_Btn_start").GetComponent<Button>();
			this.m_Btn_leave_Img = this.transform.Find("Image/e_Btn_leave").GetComponent<Image>();
			this.m_Btn_leave5_Btn = this.transform.Find("Image/e_Btn_leave").GetComponent<Button>();

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
			this.m_Btn_ready3_Btn.onClick.AddListener(this.Onm_Btn_ready3_BtnClicked);
			this.m_Btn_start4_Btn.onClick.AddListener(this.Onm_Btn_start4_BtnClicked);
			this.m_Btn_leave5_Btn.onClick.AddListener(this.Onm_Btn_leave5_BtnClicked);

		}

		private void Onm_Btn_ready3_BtnClicked()
		{
			OnButtonClicked(m_Btn_ready3_Btn.gameObject);
		}

		private void Onm_Btn_start4_BtnClicked()
		{
			OnButtonClicked(m_Btn_start4_Btn.gameObject);
		}

		private void Onm_Btn_leave5_BtnClicked()
		{
			OnButtonClicked(m_Btn_leave5_Btn.gameObject);
		}

	}
}