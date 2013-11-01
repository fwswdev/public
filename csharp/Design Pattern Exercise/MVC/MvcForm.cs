/*
 * Exercise 1: MVC @ Windows Forms
 * My interpretation of MVC Design Pattern
 * */


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MVC
{
    public partial class MvcForm : Form, IView
    {
        IController iController=null;

        public MvcForm()
        {
            InitializeComponent();
        }

        private void btnAddName_Click(object sender, EventArgs e)
        {
            iController.InsertName(txtName.Text);
        }

        private void btnCheckName_Click(object sender, EventArgs e)
        {
            bool b = iController.DoesNameExist(txtName.Text);
            MessageBox.Show(b ? "Ok" : "NotOk");
            //if (b)
            //    MessageBox.Show("Ok!");
            //else
            //    MessageBox.Show("Not Ok!");
        }

        private void MvcForm_Load(object sender, EventArgs e)
        {
            iController = new ControllerNameList();
            iController.Initialize(new ModelNameList(),this);
        }

        public void DisplayReverseName(string name)
        {
            txtRevName.Text = new String(txtName.Text.Reverse().ToArray());
        }
    }


    public interface IController
    {
        void Initialize(IModel model, IView view);
        void InsertName(string name);
        bool DoesNameExist(string name);
    }

    public interface IModel
    {
        void InsertName(string name);
        bool DoesNameExist(string name);
    }

    public interface IView
    {
        void DisplayReverseName(string name);
    }

    public class ModelNameList : IModel
    {
        List<string> myList = new List<string>();

        public void InsertName(string name)
        {
            if(name.Trim()!=String.Empty)
                myList.Add(name);
        }

        public bool DoesNameExist(string name)
        {
            name = name.Trim();
            if(name==String.Empty) return false;

            foreach (string x in myList)
                if (x == name) return true;
            return false;
        }
    }

    public class ControllerNameList : IController
    {
        IModel iModel=null;
        IView iView = null;

        public void Initialize(IModel model,IView view)
        {
            iModel = model;
            iView = view;
        }

        public void InsertName(string name)
        {
            iModel.InsertName(name);
        }

        public bool DoesNameExist(string name)
        {
            bool b = iModel.DoesNameExist(name);
            if(b)
                iView.DisplayReverseName(name);
            return b;
        }
    }
}


// eof


