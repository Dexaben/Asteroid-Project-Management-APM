
namespace Asteroid_Project_ManagerCEO.Scripts.Form
{
    public static class FormManager
    {
        public static AnaForm anaForm;
        public static System.Windows.Forms.Form AnaFormSet()
        {
            anaForm = new AnaForm();
            return anaForm;
        }
        static string sonGirilenForm;
        /// <summary>
        /// acilacak formu ANAFROM MDI'sında acar formlar_Kapatilsinmi true ise acık olan tüm formları kapatır.
        /// </summary>
        public static void FORM_AC(System.Windows.Forms.Form acilacak_Form, bool formlar_Kapatilsinmi)
        {
            if (sonGirilenForm == acilacak_Form.Name && System.Windows.Forms.Application.OpenForms[acilacak_Form.Name] != null)
            {
                Status.STATUS_LABEL("Durum:Zaten açmak istediğiniz penceredesiniz.", System.Drawing.Color.Cyan, 1000);
            }
            else
            {
                if (formlar_Kapatilsinmi)
                {
                    for (int i = 0; i < anaForm.MdiChildren.Length; i++)
                    {
                        anaForm.BeginInvoke(new System.Windows.Forms.MethodInvoker(anaForm.MdiChildren[i].Close));
                    }
                }
                acilacak_Form.MdiParent = System.Windows.Forms.Application.OpenForms["AnaForm"];
                acilacak_Form.Show();
                if (acilacak_Form.Name == new Forms.calisan().Name || acilacak_Form.Name == new Forms.AnaPanel().Name || acilacak_Form.Name == new Forms.departman().Name || acilacak_Form.Name == new sirket().Name || acilacak_Form.Name == new Forms.pozisyonlar().Name || acilacak_Form.Name == new Forms.activity().Name)
                {

                    sonGirilenForm = acilacak_Form.Name;
                }

            }
        }
    }
}
