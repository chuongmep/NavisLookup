//------------------------------------------------------------------
// NavisWorks Sample code
//------------------------------------------------------------------

// (C) Copyright 2009 by Autodesk Inc.

// Permission to use, copy, modify, and distribute this software in
// object code form for any purpose and without fee is hereby granted,
// provided that the above copyright notice appears in all copies and
// that both that copyright notice and the limited warranty and
// restricted rights notice below appear in all supporting
// documentation.

// AUTODESK PROVIDES THIS PROGRAM "AS IS" AND WITH ALL FAULTS.
// AUTODESK SPECIFICALLY DISCLAIMS ANY IMPLIED WARRANTY OF
// MERCHANTABILITY OR FITNESS FOR A PARTICULAR USE.  AUTODESK
// DOES NOT WARRANT THAT THE OPERATION OF THE PROGRAM WILL BE
// UNINTERRUPTED OR ERROR FREE.
//------------------------------------------------------------------
//
// This sample illustrates the various properties available in the API
//
//------------------------------------------------------------------

using System.Collections;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using AppInfo.Command;
using AppInfo.Events;
using AppInfo.Model;
using AppInfo.ViewModel;
using Autodesk.Navisworks.Api;
using Autodesk.Navisworks.Api.Clash;
using Autodesk.Navisworks.Internal.ApiImplementation.Schema;
using Microsoft.CSharp.RuntimeBinder;
using Application = Autodesk.Navisworks.Api.Application;

namespace AppInfo
{
    public partial class AppInfoControl : UserControl
    {
        #region enumerations

        enum TypeIcon
        {
            None = -1,
            pubclass = 0,
            pubproperty = 1,
            pubevent = 2,
            pubmethod = 3,
            staticclass = 4,
            staticproperty = 5,
            staticmethod = 6,
        }

        enum PropertyGroup
        {
            Class = 0, //properties listview
            Value = 1, //properties listview
            Property = 2, //properties listview
            Method = 3, //properties listview
            Event = 4, //events listview
            Dynamic = 5 //dynamic listview
        }

        #endregion

        public AppInfoViewModel _ViewModel { get; set; }

        public AppInfoControl(AppInfoViewModel vm)
        {
            InitializeComponent();
            _ViewModel = vm;
            ImageList imageList = new FormIcons().formIcons;
            appView.ImageList = imageList;
            propertiesView.SmallImageList = imageList;

            EventHandlers.EventRaised += EventHandlers_EventRaised;
        }

        #region delegate for member comparison

        int MemberCompare(System.Reflection.MemberInfo item1, System.Reflection.MemberInfo item2)
        {
            return item1.Name.CompareTo(item2.Name);
        }

        #endregion

        private void AppInfoControl_Load(object sender, EventArgs e)
        {
            try
            {
                Document Document = Application.ActiveDocument;
                switch (_ViewModel.SnoopType)
                {
                    case SnoopType.Application:
                        AddRootNode(typeof(Application), "Application", null);
                        TreeNode treeNode = appView.Nodes[0];
                        DisplayNodeProperties(treeNode);
                        treeNode.Expand();
                        break;
                    case SnoopType.ActiveDocument:
                        if (Document == null) return;
                        AddRootNode(Document.GetType(), nameof(Document), Document);
                        TreeNode doctree = appView.Nodes[0];
                        DisplayNodeProperties(doctree);
                        doctree.Expand();
                        break;
                    case SnoopType.CurrentSelection:
                        ModelItemCollection selectedItems = Document.CurrentSelection.SelectedItems;
                        if (selectedItems == null) return;
                        AddRootNode(selectedItems.GetType(), nameof(Document.CurrentSelection), selectedItems);
                        TreeNode selecttree = appView.Nodes[0];
                        DisplayNodeProperties(selecttree);
                        selecttree.Expand();
                        break;
                    case SnoopType.ElementId:
                        break;
                    case SnoopType.Search:
                        DocumentClashTests clashTests = Application.MainDocument.GetClash().TestsData;
                        if (clashTests == null) return;
                        if(clashTests.Tests.Count==0) return;
                        SavedItem clashResult = null;
                        foreach (var savedItem1 in clashTests.Tests)
                        {
                            var savedItem = (ClashTest) savedItem1;
                            if (savedItem == null) continue;
                            SavedItemCollection savedItemCollection = savedItem.Children;
                            switch (_ViewModel.SearchType)
                            {
                                case NodeSearch.SearchType.ClashResultGuid:
                                    
                                    foreach (SavedItem item in savedItemCollection)
                                    {
                                        if (item == null) continue;
                                        if (item is ClashResult)
                                        {
                                            ClashResult result = item as ClashResult;
                                            if(result.Guid.ToString().ToLower() == _ViewModel.SearchValue)
                                            {
                                                clashResult = result;
                                                break;
                                            }
                                        }
                                        else if (item is ClashResultGroup clashResultGroup)
                                        {
                                            foreach (var clashResultItem in clashResultGroup.Children)
                                            {
                                                ClashResult resultItem = clashResultItem as ClashResult;
                                                if(resultItem != null)
                                                {
                                                    if (clashResult.Guid.ToString().ToLower() == _ViewModel.SearchValue)
                                                    {
                                                        clashResult = resultItem;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    };
                                    break;
                                case NodeSearch.SearchType.ClashResultName:
                                    foreach (SavedItem item in savedItemCollection)
                                    {
                                        if (item == null) continue;
                                        if (item is ClashResult result)
                                        {
                                            if (result.DisplayName.ToLower() == _ViewModel.SearchValue)
                                            {
                                                clashResult = result;
                                                break;
                                            }
                                        }
                                        else if (item is ClashResultGroup clashResultGroup)
                                        {
                                            foreach (var clashResultItem in clashResultGroup.Children)
                                            {
                                                ClashResult resultItem = clashResultItem as ClashResult;
                                                if (resultItem != null)
                                                {
                                                    if (resultItem.DisplayName.ToLower() == _ViewModel.SearchValue)
                                                    {
                                                        clashResult = resultItem;
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    };
                                    break;
                                default:
                                    clashResult = null;
                                    break;
                            }
                        }
                        if (clashResult == null)
                        {
                            return;
                        }
                        AddRootNode(clashResult.GetType(), "ClashResult", clashResult);
                        TreeNode clashResultTree = appView.Nodes[0];
                        DisplayNodeProperties(clashResultTree);
                        clashResultTree.Expand();
                        break;
                    case SnoopType.ActiveView:
                        Autodesk.Navisworks.Api.View activeView = Document.ActiveView;
                        if (activeView == null) return;
                        AddRootNode(activeView.GetType(), nameof(Document.ActiveView), activeView);
                        TreeNode viewtree = appView.Nodes[0];
                        DisplayNodeProperties(viewtree);
                        viewtree.Expand();
                        break;
                    case SnoopType.ActiveSheet:
                        SheetInfo sheetInfo = Document.ActiveSheet;
                        if (sheetInfo == null) return;
                        AddRootNode(sheetInfo.GetType(), nameof(Document.ActiveSheet), sheetInfo);
                        TreeNode viewsheettree = appView.Nodes[0];
                        DisplayNodeProperties(viewsheettree);
                        viewsheettree.Expand();
                        break;
                    case SnoopType.ClashTest:
                        DocumentClashTests dct = Application.MainDocument.GetClash().TestsData;
                        if (dct == null) return;
                        AddRootNode(dct.GetType(), "DocumentClashTests", dct);
                        TreeNode clashitemtree = appView.Nodes[0];
                        DisplayNodeProperties(clashitemtree);
                        clashitemtree.Expand();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Autodesk.Navisworks.Api.Application.Gui.MainWindow, ex.Message);
            }
        }

        public void CleanUp()
        {
            EventHandlers.RemoveAllEventHandlers();
            while (appView.TopNode != null)
            {
                ClearNodes(appView.TopNode);
                appView.Nodes.Remove(appView.TopNode);
            }
        }

        private void AppInfoControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                foreach (TreeNode treeNode in appView.Nodes)
                {
                    if (treeNode.IsExpanded)
                    {
                        treeNode.Collapse();
                    }
                }
            }
        }

        private void appView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Node.Nodes.Clear();
            ExpandNode(e.Node);
        }

        private void appView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            ClearChildNodes(e.Node);
            e.Node.Nodes.Clear();
            e.Node.Nodes.Add("Working...");
        }

        private void appView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Clear the node information that is displayed in the listvews
            ClearNodeInfo();

            //check the node's information and update if needed.
            if (!CheckNodeIsValid(e.Node) && e.Node.IsExpanded)
            {
                ClearChildNodes(e.Node);
                e.Node.Nodes.Clear();
                ExpandNode(e.Node);
            }

            //Check in case the node was expanded but with no children previously
            if (e.Node.Nodes.Count == 0 && !e.Node.IsExpanded)
            {
                e.Node.Nodes.Clear();
                e.Node.Nodes.Add("Working...");
            }

            //display node information
            try
            {
                DisplayNodeProperties(e.Node);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        #region Events ListView Event Handlers

        private void attachToEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeInfo nodeInfo = appView.SelectedNode.Tag as NodeInfo;
            if (nodeInfo != null &&
                eventsView.SelectedItems.Count > 0)
            {
                AttachEventHandler(eventsView.SelectedItems[0]);
            }
        }

        private void AttachEventHandler(ListViewItem lvi)
        {
            if(appView.SelectedNode==null) return;
            NodeInfo nodeInfo = appView.SelectedNode.Tag as NodeInfo;
            if (nodeInfo != null)
            {
                EventInfo info = lvi.Tag as EventInfo;
                if (info != null)
                {
                    if (!EventHandlers.AddEventHandler(nodeInfo.Type, nodeInfo.Value, lvi.Text, info))
                    {
                        MessageBox.Show(this, "Unable to attach to event", "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void detachFromEventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NodeInfo nodeInfo = appView.SelectedNode.Tag as NodeInfo;
            if (nodeInfo != null &&
                eventsView.SelectedItems.Count > 0)
            {
                DetachEventHandler(eventsView.SelectedItems[0]);
            }
        }

        private void DetachEventHandler(ListViewItem lvi)
        {
            if(appView.SelectedNode==null) return;
            NodeInfo nodeInfo = appView.SelectedNode.Tag as NodeInfo;
            if (nodeInfo != null)
            {
                EventInfo info = lvi.Tag as EventInfo;
                if (info != null)
                {
                    if (!EventHandlers.RemoveEventHandler(nodeInfo.Type, nodeInfo.Value, lvi.Text, info))
                    {
                        MessageBox.Show(this, "Unable to detach from event", "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void eventsView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (eventsView.SelectedItems.Count > 0)
                {
                    if (eventsView.SelectedItems[0].Tag is EventInfo)
                    {
                        NodeInfo nodeInfo = appView.SelectedNode.Tag as NodeInfo;
                        if (nodeInfo != null)
                        {
                            attachToEventToolStripMenuItem.Enabled = !EventHandlers.ContainsKey(nodeInfo.Type,
                                nodeInfo.Value, eventsView.SelectedItems[0].Text);
                            detachFromEventToolStripMenuItem.Enabled = !attachToEventToolStripMenuItem.Enabled;
                        }

                        contextMenuStrip.Show(eventsView, e.Location);
                    }
                }
            }
        }

        protected bool IsEventHandled(ListViewItem lvi, TreeNode treeNode)
        {
            bool retVal = false;
            if (lvi.Tag is EventInfo)
            {
                retVal = IsEventHandled(lvi.Text, treeNode);
            }

            return retVal;
        }

        protected bool IsEventHandled(string eventName, TreeNode treeNode)
        {
            bool retVal = false;
            NodeInfo nodeInfo = treeNode.Tag as NodeInfo;
            if (nodeInfo != null)
            {
                retVal = EventHandlers.ContainsKey(nodeInfo.Type, nodeInfo.Value, eventName);
            }

            return retVal;
        }

        private void eventsView_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.CurrentValue != e.NewValue)
            {
                ListViewItem lvi = eventsView.Items[e.Index];
                if (e.NewValue == CheckState.Checked)
                    AttachEventHandler(lvi);
                else
                    DetachEventHandler(lvi);
            }
        }

        void EventHandlers_EventRaised(object sender, EventDetailsArgs e)
        {
            foreach (ListViewItem lvi in eventsView.Items)
            {
                if (e.EventDetails.EventInfo == (EventInfo) lvi.Tag)
                {
                    lvi.SubItems[1].Text =
                        e.EventDetails.LastRaised.ToString();
                }
            }

            StringBuilder s = new StringBuilder();
            s.Append(e.EventDetails.CallerType.Name);
            s.Append(".");
            s.Append(e.EventDetails.EventName);
            s.Append(" <");
            s.Append(e.EventArgs.GetType().Name.ToString());
            s.AppendLine(">");
            s.Append("\tsender = ");
            s.AppendLine(ResolveName(sender != null ? sender.ToString() : "null"));

            PropertyInfo[] propertyInfo = e.EventArgs.GetType().GetProperties(BindingFlags.Instance |
                                                                              BindingFlags.FlattenHierarchy |
                                                                              BindingFlags.Public |
                                                                              BindingFlags.Static);
            // sort by name
            Array.Sort(propertyInfo, MemberCompare);

            foreach (PropertyInfo info in propertyInfo)
            {
                try
                {
                    if (info.CanRead)
                    {
                        object[] attributes =
                            info.GetCustomAttributes(typeof(System.ComponentModel.EditorBrowsableAttribute), true);
                        if (attributes == null || attributes.Length == 0)
                        {
                            s.Append("\t");
                            s.Append(e.EventArgs.GetType().Name.ToString());
                            s.Append(".");
                            s.Append(info.Name);
                            s.Append(" = ");
                            object val = info.GetValue((object) e.EventArgs, null);
                            s.Append((val == null) ? "null" : val.ToString());
                        }
                    }
                }
                catch (Exception)
                {
                }

                s.AppendLine();
            }

            output.AppendText(s.ToString());
            output.ScrollToCaret();
        }

        #endregion

        /// <summary>
        /// Clear the associated object with this node and all children
        /// </summary>
        /// <param name="currNode"></param>
        private void ClearNodes(TreeNode currNode)
        {
            ClearChildNodes(currNode);
            if (currNode.Tag != null && currNode.Tag is NodeInfo)
                ((NodeInfo) currNode.Tag).Dispose();
            currNode.Tag = null;
        }

        /// <summary>
        /// Clear the associated object with only the children of this node
        /// </summary>
        /// <param name="currNode"></param>
        private void ClearChildNodes(TreeNode currNode)
        {
            foreach (TreeNode child in currNode.Nodes)
            {
                ClearNodes(child);
            }
        }

        private bool CheckNodeIsValid(TreeNode currNode)
        {
            if (currNode.Parent == null)
                return true;

            //Get parent Information
            TreeNode parentNode = currNode.Parent;
            bool isParentDisposed;
            bool isParentStatic;
            object parentObj;
            int parentIndex;
            MemberInfo parentInfo;
            Type parentType = GetTypeInformation(parentNode, out parentObj, out isParentDisposed, out isParentStatic,
                out parentIndex, out parentInfo);

            //get current node Information
            bool isCurrDisposed;
            bool isCurrStatic;
            object currObj;
            int currIndex;
            MemberInfo currInfo;
            Type nodeType;
            Type currType = GetTypeInformation(currNode, out currObj, out isCurrDisposed, out isCurrStatic,
                out currIndex, out currInfo, out nodeType);

            //return value
            bool retVal = false;

            //object for comparison
            object obj = null;

            if (currInfo != null)
            {
                try
                {
                    if (currInfo is FieldInfo)
                    {
                        //public fields
                        FieldInfo fieldInfo = parentType.GetField(currNode.Text);
                        obj = fieldInfo.GetValue(parentObj);
                    }
                    else if (currInfo is PropertyInfo)
                    {
                        //Properties
                        PropertyInfo propertyInfo = parentType.GetProperty(currNode.Text, currType);
                        if (propertyInfo == null)
                        {
                            propertyInfo = parentType.GetProperty(currNode.Text, nodeType);
                        }

                        obj = propertyInfo.GetValue(parentObj, null);
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }

                //compare objects
                retVal = (obj == currObj || (obj != null && obj.Equals(currObj)));
            }

            System.Collections.IEnumerable collection = parentObj as System.Collections.IEnumerable;
            if (collection != null)
            {
                foreach (object tempObj in collection)
                {
                    if (currIndex-- == 0)
                    {
                        obj = tempObj;
                        currType = obj.GetType();
                        retVal = (obj == currObj || (obj != null && obj.Equals(currObj)));
                        break;
                    }
                }
            }

            //if objects difer update the node
            if (!retVal && obj != null)
            {
                UpdateNodeObject(currNode, currType, obj);
            }

            return retVal;
        }

        private static void AddIEnumerable(TreeNode currNode, object currObj)
        {
            System.Collections.IEnumerable collection = currObj as System.Collections.IEnumerable;
            if (collection == null) return;

            int i = 0;
            foreach (object obj in collection)
            {
                if (obj == null ||
                    (obj is Autodesk.Navisworks.Api.NativeHandle &&
                     ((Autodesk.Navisworks.Api.NativeHandle) obj).IsDisposed))
                {
                    continue;
                }
                else
                {
                    try
                    {
                        AddNode(currNode,
                            obj.GetType().ToString().Replace(obj.GetType().Namespace + ".", "") + "[" + i + "]",
                            obj, obj.GetType(), TypeIcon.pubclass, false, null, i++);
                    }
                    catch (ObjectDisposedException)
                    {
                    }
                }
            }
        }

        private static void AddNode(TreeNode parentNode, string nodeText, object o, Type type, TypeIcon imageIndex,
            bool isStatic, MemberInfo info)
        {
            AddNode(parentNode, nodeText, o, type, imageIndex, isStatic, info, -1);
        }

        private static void AddNode(TreeNode parentNode, object parentObj, PropertyInfo info)
        {
            try
            {
                object o = info.GetValue(parentObj, null);
                if (o == null) return;
                if (info.CanRead && info.GetGetMethod().IsStatic ||
                    info.CanWrite && info.GetSetMethod().IsStatic)
                {
                    AddNode(parentNode, info.Name, o, info.PropertyType, TypeIcon.staticproperty,
                        (info.CanRead && info.GetGetMethod().IsStatic || info.CanWrite && info.GetSetMethod().IsStatic),
                        info);
                }
                else
                {
                    AddNode(parentNode, info.Name, o, info.PropertyType, TypeIcon.pubproperty,
                        (info.CanRead && info.GetGetMethod().IsStatic || info.CanWrite && info.GetSetMethod().IsStatic),
                        info);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private static void AddNode(TreeNode parentNode, object parentObj, FieldInfo info)
        {
            try
            {
                object o = info.GetValue(parentObj);
                if (info.IsStatic)
                {
                    AddNode(parentNode, info.Name, o, info.FieldType, TypeIcon.staticproperty, info.IsStatic, info);
                }
                else
                {
                    AddNode(parentNode, info.Name, o, info.FieldType, TypeIcon.pubproperty, info.IsStatic, info);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
            }
        }

        private static void AddNode(TreeNode parentNode, string nodeText, object o, Type type, TypeIcon imageIndex,
            bool isStatic, MemberInfo info, int index)
        {
            TreeNode childNode = parentNode.Nodes.Add(nodeText);
            childNode.ImageIndex = (int) imageIndex;
            childNode.SelectedImageIndex = (int) imageIndex;
            childNode.Tag = new NodeInfo(type, o, isStatic, info, index);
            childNode.Nodes.Add("Working...");
        }

        public void UpdateNodeObject(TreeNode treeNode, Type type, object instance)
        {
            NodeInfo nodeInfo = treeNode.Tag as NodeInfo;
            if (nodeInfo != null)
            {
                nodeInfo.Value = instance;
                nodeInfo.Type = type;
            }
        }

        public void AddRootNode(Type rootType, string title, object instance)
        {
            TreeNode childNode = appView.Nodes.Add(title);
            childNode.SelectedImageIndex = (int) ((instance == null) ? TypeIcon.staticclass : TypeIcon.pubclass);
            childNode.ImageIndex = childNode.SelectedImageIndex;
            childNode.Tag = new NodeInfo(rootType, instance, (instance == null), null, -1);
            childNode.Nodes.Add("Working...");
        }

        #region Node Information

        private void ExpandNode(TreeNode currNode)
        {
            try
            {
                if (currNode != null)
                {
                    currNode.Nodes.Clear();

                    object currObj = null;
                    Type currType = null;
                    bool isDisposed = false;
                    bool isStatic = false;
                    currType = GetTypeInformation(currNode, out currObj, out isDisposed, out isStatic);

                    if (currType != null && !isDisposed)
                    {
                        //public fields
                        FieldInfo[] fieldInfo = currType.GetFields(BindingFlags.Instance |
                                                                   BindingFlags.FlattenHierarchy | BindingFlags.Public |
                                                                   BindingFlags.Static);
                        // sort by name
                        Array.Sort(fieldInfo, MemberCompare);

                        foreach (FieldInfo info in fieldInfo)
                        {
                            if (IsValidForTree(info, info.FieldType))
                            {
                                AddNode(currNode, currObj, info);
                            }
                        }


                        //public properties
                        PropertyInfo[] propertyInfo = currType.GetProperties(BindingFlags.Instance |
                                                                             BindingFlags.FlattenHierarchy |
                                                                             BindingFlags.Public | BindingFlags.Static);
                        // sort by name
                        Array.Sort(propertyInfo, MemberCompare);
                        foreach (PropertyInfo info in propertyInfo)
                        {
                            if (info.CanRead)
                            {
                                if (IsValidForTree(info, info.PropertyType))
                                {
                                    AddNode(currNode, currObj, info);
                                }
                            }
                        }

                        //Add enumerables

                        AddIEnumerable(currNode, currObj);

                        foreach (GetMemberBinder binder in GetDynamicMemberBinders(currObj))
                        {
                            CallSite<Func<CallSite, object, object>> site =
                                CallSite<Func<CallSite, object, object>>.Create(binder);
                            var sub_obj = site.Target(site, currObj);
                            Type sought_type = typeof(GroupAccess);
                            if (sub_obj.GetType().IsRelatedTo(ref sought_type))
                            {
                                AddNode(currNode, binder.Name, sub_obj, sought_type, TypeIcon.pubproperty, false, null);
                            }

                            Type avoid_type = typeof(VectorSimpleReader<>);
                            Type avoid_type2 = typeof(VectorSimpleEditor<>);
                            sought_type = typeof(ArrayAccess<,>);
                            if (!sub_obj.GetType().IsRelatedTo(ref avoid_type) &&
                                !sub_obj.GetType().IsRelatedTo(ref avoid_type2) &&
                                sub_obj.GetType().IsRelatedTo(ref sought_type))
                            {
                                IList list = (IList) sub_obj;
                                for (int i = 0; i < list.Count; ++i)
                                {
                                    AddNode(currNode, binder.Name + "[" + i.ToString() + "]", sub_obj, sought_type,
                                        TypeIcon.pubproperty, false, null);
                                }
                            }
                        }
                    }
                }
            }
            catch (ObjectDisposedException)
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        private void DisplayNodeProperties(TreeNode currNode)
        {
            if (currNode != null)
            {
                Type currType = null;
                object currObj = null;
                bool isDisposed = false;
                bool isStatic = false;

                currType = GetTypeInformation(currNode, out currObj, out isDisposed, out isStatic);

                if (isDisposed && currType != null)
                {
                    //class name
                    AddToListView(propertiesView, PropertyGroup.Class, isStatic, currType.ToString(), null);

                    //object value
                    AddToListView(propertiesView, PropertyGroup.Value, isStatic, "DISPOSED", null);
                }
                else if (currType != null)
                {
                    //class name
                    AddToListView(propertiesView, PropertyGroup.Class, isStatic, currType.ToString(), null);

                    AddToListView(propertiesView, PropertyGroup.Value, isStatic,
                        (currObj != null ? currObj.ToString() : "null"), null);

                    DisplayFields(currType, currObj);

                   DisplayProperties(currType, currObj);

                    DisplayMethods(currType);

                    DisplayDynamicMembers(currObj);

                    DisplayEvents(currType, currObj);
                }
            }
        }

        private void DisplayFields(Type currType, object currObj)
        {
            //Fields
            FieldInfo[] fieldInfo = currType.GetFields(BindingFlags.Instance | BindingFlags.FlattenHierarchy |
                                                       BindingFlags.Public | BindingFlags.Static);
            // sort by name
            Array.Sort(fieldInfo, MemberCompare);

            foreach (FieldInfo info in fieldInfo)
            {
                try
                {
                    AddToListView(propertiesView, PropertyGroup.Property, info.IsStatic, info.Name,
                        info.GetValue(currObj).ToString(), null);
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }

        private void DisplayProperties(Type currType, object currObj)
        {
            //Properties
            PropertyInfo[] propertyInfo = currType.GetProperties(BindingFlags.Instance | BindingFlags.FlattenHierarchy |
                                                                 BindingFlags.Public | BindingFlags.Static);
            // sort by name
            Array.Sort(propertyInfo, MemberCompare);

            foreach (PropertyInfo info in propertyInfo)
            {
                try
                {
                    if (info.CanRead)
                    {
                        object[] attributes =
                            info.GetCustomAttributes(typeof(System.ComponentModel.EditorBrowsableAttribute), true);
                        bool browsable = true;
                        if (attributes.Length > 0)
                        {
                            // is this browsable?
                            System.Diagnostics.Debug.Assert(attributes.Length == 1);
                            System.ComponentModel.EditorBrowsableAttribute attrib =
                                attributes[0] as System.ComponentModel.EditorBrowsableAttribute;
                            System.Diagnostics.Debug.Assert(attrib != null);
                            browsable = (attrib.State == System.ComponentModel.EditorBrowsableState.Always);
                        }

                        if (browsable)
                        {
                            bool is_static = (info.CanRead && info.GetGetMethod() != null &&
                                              info.GetGetMethod().IsStatic);
                            is_static |= (info.CanWrite && info.GetSetMethod() != null && info.GetSetMethod().IsStatic);
                            if (info.Name.Equals("PreferredSize"))
                            {
                                // Fix bug background color ribbon to black
                                AddToListView(propertiesView, PropertyGroup.Property, is_static, info.Name, null,
                                    string.Empty);
                                continue;
                            }
                            object current_object = info.GetValue(currObj, null);
                            string current_value = (current_object != null) ? current_object.ToString() : "null";

                            AddToListView(propertiesView, PropertyGroup.Property, is_static, info.Name, null,
                                current_value); 
                           
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }

        private void DisplayDynamicMembers(object currObj)
        {
            foreach (GetMemberBinder binder in GetDynamicMemberBinders(currObj))
            {
                CallSite<Func<CallSite, object, object>> site = CallSite<Func<CallSite, object, object>>.Create(binder);
                AddToListView(propertiesView, PropertyGroup.Dynamic, false, binder.Name, null,
                    ConvertDynamicObject(site.Target(site, currObj)));
            }
        }

        private IEnumerable<GetMemberBinder> GetDynamicMemberBinders(object obj)
        {
            if (obj != null && obj.GetType().IsImplementationOf(typeof(IDynamicMetaObjectProvider)))
            {
                IDynamicMetaObjectProvider provider = (IDynamicMetaObjectProvider) obj;
                ParameterExpression param = Expression.Parameter(typeof(IDynamicMetaObjectProvider));
                DynamicMetaObject meta = provider.GetMetaObject(param);
                IEnumerable<string> members = meta.GetDynamicMemberNames();
                foreach (string member in members)
                {
                    GetMemberBinder binder = (GetMemberBinder) Microsoft.CSharp.RuntimeBinder.Binder.GetMember(
                        CSharpBinderFlags.None, member, this.GetType(),
                        new[] {CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)});
                    yield return binder;
                }
            }
        }

        private string ConvertDynamicObject(object dynamic_obj)
        {
            Type sought_type = typeof(SimpleAccess<,>);
            if (dynamic_obj.GetType().IsRelatedTo(ref sought_type))
            {
                var converter = sought_type.GetMethod("op_Implicit", new[] {sought_type});
                if (converter != null)
                {
                    var obj = converter.Invoke(null, new[] {dynamic_obj});
                    return obj.ToString();
                }
            }

            sought_type = typeof(VectorSimpleReader<>);
            Type sought_type2 = typeof(VectorSimpleEditor<>);
            if (dynamic_obj.GetType().IsRelatedTo(ref sought_type) ||
                dynamic_obj.GetType().IsRelatedTo(ref sought_type2))
            {
                StringBuilder str_builder = new StringBuilder();
                IEnumerable list = (IEnumerable) dynamic_obj;
                foreach (object list_value in list)
                {
                    str_builder.Append(list_value);
                    str_builder.Append(", ");
                }

                str_builder.Remove(str_builder.Length - 2, 2);
                return str_builder.ToString();
            }

            return dynamic_obj.ToString();
        }

        private void DisplayMethods(Type currType)
        {
            //Methods
            System.Reflection.MethodInfo[] methodInfo = currType.GetMethods(BindingFlags.Instance |
                                                                            BindingFlags.FlattenHierarchy |
                                                                            BindingFlags.Public | BindingFlags.Static);
            // sort by name
            Array.Sort(methodInfo, MemberCompare);

            foreach (System.Reflection.MethodInfo info in methodInfo)
            {
                try
                {
                    if (!info.IsSpecialName && info.DeclaringType != typeof(object))
                    {
                        object[] attributes =
                            info.GetCustomAttributes(typeof(System.ComponentModel.EditorBrowsableAttribute), true);
                        if (attributes.Length == 0)
                        {
                            AddToListView(propertiesView, PropertyGroup.Method, info.IsStatic, info.ToString(), null);
                        }
                    }
                }
                catch (Exception e)
                {
                    Trace.WriteLine(e.Message);
                }
            }
        }

        private void DisplayEvents(Type currType, object currObj)
        {
            //Events
            System.Reflection.EventInfo[] eventInfo = currType.GetEvents(BindingFlags.Instance |
                                                                         BindingFlags.FlattenHierarchy |
                                                                         BindingFlags.Public | BindingFlags.Static);
            // sort by name
            Array.Sort(eventInfo, MemberCompare);

            //temporarily turn off the checked event until we've displayed the events
            eventsView.ItemCheck -= eventsView_ItemCheck;

            foreach (System.Reflection.EventInfo info in eventInfo)
            {
                try
                {
                    string statusText;
                    bool eventHandled = EventHandlers.GetStatusText(currType, currObj, info.Name, out statusText);
                    ListViewItem lvi = AddToListView(eventsView, PropertyGroup.Event, false, info.Name, info,
                        statusText);
                    lvi.Checked = eventHandled;
                }
                catch (Exception)
                {
                }
            }

            //re-enable events
            eventsView.ItemCheck += eventsView_ItemCheck;
        }

        private void ClearNodeInfo()
        {
            propertiesView.Items.Clear();
            eventsView.Items.Clear();
        }

        private ListViewItem AddToListView(ListView lv, PropertyGroup group, bool isStatic, string column1, object tag,
            params string[] columns)
        {
            ListViewItem lvi = new ListViewItem(ResolveName(column1));
            lvi.Tag = tag;
            switch (group)
            {
                case PropertyGroup.Class:
                    lvi.ImageIndex = (int) ((isStatic) ? TypeIcon.staticclass : TypeIcon.pubclass);
                    lvi.Group = propertiesView.Groups[0];
                    break;
                case PropertyGroup.Property:
                    lvi.Group = propertiesView.Groups[2];
                    lvi.ImageIndex = (int) ((isStatic) ? TypeIcon.staticproperty : TypeIcon.pubproperty);
                    break;
                case PropertyGroup.Dynamic:
                    lvi.Group = propertiesView.Groups[4];
                    lvi.ImageIndex = (int) ((isStatic) ? TypeIcon.staticproperty : TypeIcon.pubproperty);
                    break;
                case PropertyGroup.Method:
                    lvi.Group = propertiesView.Groups[3];
                    lvi.ImageIndex = (int) ((isStatic) ? TypeIcon.staticmethod : TypeIcon.pubmethod);
                    break;
                case PropertyGroup.Event:
                    lvi.ImageIndex = (int) TypeIcon.pubevent;
                    break;
                case PropertyGroup.Value:
                    lvi.Group = propertiesView.Groups[1];
                    lvi.ImageIndex = (int) TypeIcon.None;
                    break;
                default:
                    lvi.ImageIndex = (int) TypeIcon.None;
                    break;
            }

            if (columns == null) return lvi;
            foreach (string column in columns)
            {
                lvi.SubItems.Add(ResolveName(column));
            }

            lv.Items.Add(lvi);

            return lvi;
        }

        #endregion

        private static Regex _regex =
            new Regex(
                "(.*?System\\.Collections\\.(Generic.+\\.*|ObjectModel\\..*Collection\\.*)?)(`[0-9]+|\\^)(\\[)(.+)(\\])");

        private static string ResolveName(string inStr)
        {
            if (inStr == null)
                return "null";
            inStr = _regex.Replace(inStr.ToString(), "$1<$5>");
            return inStr;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="currObj"></param>
        /// <param name="isDisposed"></param>
        /// <param name="isStatic"></param>
        /// <returns></returns>
        private Type GetTypeInformation(TreeNode currNode, out object currObj, out bool isDisposed, out bool isStatic)
        {
            int index;
            return GetTypeInformation(currNode, out currObj, out isDisposed, out isStatic, out index);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="currObj"></param>
        /// <param name="isDisposed"></param>
        /// <param name="isStatic"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private Type GetTypeInformation(TreeNode currNode, out object currObj, out bool isDisposed, out bool isStatic,
            out int index)
        {
            MemberInfo info;
            return GetTypeInformation(currNode, out currObj, out isDisposed, out isStatic, out index, out info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="currObj"></param>
        /// <param name="isDisposed"></param>
        /// <param name="isStatic"></param>
        /// <param name="index"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        private Type GetTypeInformation(TreeNode currNode, out object currObj, out bool isDisposed, out bool isStatic,
            out int index, out MemberInfo info)
        {
            Type nodeType;
            return GetTypeInformation(currNode, out currObj, out isDisposed, out isStatic, out index, out info,
                out nodeType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currNode"></param>
        /// <param name="currObj"></param>
        /// <param name="isDisposed"></param>
        /// <param name="isStatic"></param>
        /// <param name="index"></param>
        /// <param name="info"></param>
        /// <param name="nodeType"></param>
        /// <returns></returns>
        private Type GetTypeInformation(TreeNode currNode, out object currObj, out bool isDisposed, out bool isStatic,
            out int index, out MemberInfo info, out Type nodeType)
        {
            Type currType = null;
            isDisposed = true;
            isStatic = false;
            currObj = null;
            info = null;
            index = -1;
            nodeType = null;
            NodeInfo nodeInfo = currNode.Tag as NodeInfo;
            try
            {
                currObj = nodeInfo.Value;
                nodeType = nodeInfo.Type;

                //check obj isnt null, this also takes care of instances where the type is an interface 
                //derived class. when its non null we get the REAL object type.
                if (currObj == null)
                    currType = nodeInfo.Type;
                else
                    currType = currObj.GetType();

                isStatic = nodeInfo.IsStatic;
                info = nodeInfo.Info;

                if (currObj is Autodesk.Navisworks.Api.NativeHandle &&
                    ((Autodesk.Navisworks.Api.NativeHandle) currObj).IsDisposed)
                {
                    isDisposed = true;
                }
                else
                    isDisposed = false;
            }
            catch (ObjectDisposedException)
            {
                try
                {
                    nodeInfo.Value = null;
                    isDisposed = true;
                    isStatic = false;
                    info = null;
                }
                catch (ObjectDisposedException)
                {
                    currNode.Tag = null;
                }
            }

            return currType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsValidForTree(MemberInfo info, Type type)
        {
            object[] attributes =
                info.GetCustomAttributes(typeof(System.ComponentModel.EditorBrowsableAttribute), true);
            return !type.IsValueType &&
                   type.IsPublic && type != (typeof(System.String)) &&
                   (attributes == null || attributes.Length == 0);
        }

        private void clearOutputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Clear();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.SelectAll();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            output.Copy();
        }
    }
}