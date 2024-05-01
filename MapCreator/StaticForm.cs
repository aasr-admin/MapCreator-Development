namespace MapCreator
{
    public static class StaticForm<T> where T : Form, new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance?.IsDisposed != false)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public static T Hide()
        {
            var form = _instance;

            if (form?.IsDisposed != false)
            {
                return null;
            }

            form.SendToBack();

            if (form.Visible)
            {
                form.Hide();
            }

            return form;
        }

        public static T Open(Form owner)
        {
            var form = Instance;

            if (form?.IsDisposed != false)
            {
                return null;
            }

            if (!form.Visible)
            {
                form.Show(owner);
            }
            else
            {
                form.Owner = owner;
            }

            form.BringToFront();

            return form;
        }

        public static T Open()
        {
            var form = Instance;

            if (form?.IsDisposed != false)
            {
                return null;
            }

            if (!form.Visible)
            {
                form.Show(form.Owner);
            }

            form.BringToFront();

            return form;
        }
    }
}
