using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{

	public class Lobby_UIBase : UIBase
	{
		protected Transform m_grid_Trans;
		protected Transform m_grid1_Trans;
		protected Toggle m_Toggle_Tgl;
		protected Text m_Txt_info_Txt;
		protected Image m_Btn_EnterRoom_Img;
		protected Button m_Btn_EnterRoom2_Btn;
		protected Image m_Btn_Create_Img;
		protected Button m_Btn_Create3_Btn;
		protected Image m_InputRoomName_Img;
		protected InputField m_InputRoomName4_IptField;

		protected ActionManager ActionOnShow = new ActionManager();

		private void Start()
		{
			this.m_grid_Trans = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid").GetComponent<Transform>();
			this.m_grid1_Trans = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid").GetComponent<Transform>();
			this.m_Toggle_Tgl = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid/Template/e_Toggle").GetComponent<Toggle>();
			this.m_Txt_info_Txt = this.transform.Find("Image/Scroll View/Viewport/Content/e_grid/Template/e_Toggle/e_Txt_info").GetComponent<Text>();
			this.m_Btn_EnterRoom_Img = this.transform.Find("Image/e_Btn_EnterRoom").GetComponent<Image>();
			this.m_Btn_EnterRoom2_Btn = this.transform.Find("Image/e_Btn_EnterRoom").GetComponent<Button>();
			this.m_Btn_Create_Img = this.transform.Find("Image/e_Btn_Create").GetComponent<Image>();
			this.m_Btn_Create3_Btn = this.transform.Find("Image/e_Btn_Create").GetComponent<Button>();
			this.m_InputRoomName_Img = this.transform.Find("Image/e_InputRoomName").GetComponent<Image>();
			this.m_InputRoomName4_IptField = this.transform.Find("Image/e_InputRoomName").GetComponent<InputField>();

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
			this.m_Toggle_Tgl.onValueChanged.AddListener(this.Onm_Toggle_TglValueChanged);
			this.m_Btn_EnterRoom2_Btn.onClick.AddListener(this.Onm_Btn_EnterRoom2_BtnClicked);
			this.m_Btn_Create3_Btn.onClick.AddListener(this.Onm_Btn_Create3_BtnClicked);
			this.m_InputRoomName4_IptField.onValueChanged.AddListener(this.Onm_InputRoomName4_IptFieldValueChanged);

		}

		private void Onm_Toggle_TglValueChanged(bool arg)
		{

		}

		private void Onm_Btn_EnterRoom2_BtnClicked()
		{
			OnButtonClicked(m_Btn_EnterRoom2_Btn.gameObject);
		}

		private void Onm_Btn_Create3_BtnClicked()
		{
			OnButtonClicked(m_Btn_Create3_Btn.gameObject);
		}

		private void Onm_InputRoomName4_IptFieldValueChanged(string arg)
		{

		}

	}
}