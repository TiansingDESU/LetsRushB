using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    class Battle_UILogic : Battle_UIBase
    {
        public override void OnShow(object param)
        {
            base.OnShow(param);
            ActionOnShow.RegAction(LevelManager.instance.OnLifeChange, OnLifeChange);
            ActionOnShow.RegAction(LevelManager.instance.OnShowLevelEnd, OnLevelEnd);

            m_Img_end_Img.gameObject.SetActive(false);
            m_Txt_blue_Txt.text = m_Txt_red_Txt.text = string.Empty;
        }

        private void OnLifeChange()
        {
            int blue = LevelManager.instance.TeamBlueLifes;
            int red = LevelManager.instance.TeamRedLifes;
            m_Txt_blue_Txt.text = "Life : " + blue;
            m_Txt_red_Txt.text = "Life : " + red;
        }

        private void OnLevelEnd()
        {
            m_Img_end_Img.gameObject.SetActive(true);
            if(LevelManager.instance.WinTeam == LevelManager.TeamType.BlueTeam)
            {
                m_Txt_end_Txt.text = "Blue Team Win";
            }
            else
            {
                m_Txt_end_Txt.text = "Red Team Win";
            }
        }
    }
}
