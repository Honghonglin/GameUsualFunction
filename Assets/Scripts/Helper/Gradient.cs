using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace UI.Extension
{

    /*
     
     需要注意的是使用了这个渐变组件的文本不能同时嵌套两个同样的标签
    因为是使用正则去匹配的，如果嵌套会出问题

    TODO：使用语法树+栈的实现方法   重写Text
     */
    public class ContentItem
    {
        public int startIndex;
        public int length;
        public List<Color> gradientColor = new List<Color>();
    }
    [AddComponentMenu("UI/Effects/Gradient")]
    [RequireComponent(typeof(Text))]
    [ExecuteInEditMode]
    public class Gradient : BaseMeshEffect
    {
        public List<string> m_patterts;

        public bool MultiplyTextColor = false;
        private Text txt;
        private string initStr;
        private List<ContentItem> contentItems = new List<ContentItem>();
        List<UIVertex> verts=new List<UIVertex>();

        private string removeOtherTxt;
        private string removeGradientTxt;
        public void SetText(string str)
        {
            txt.text = str;
            InitText();
        }
        protected override void Start()
        {
            if (txt == null)
            {
                txt = transform.GetComponent<Text>();
                initStr = txt.text;
            }
            InitText();
        }

        private void InitText()
        {
            if (txt == null)
            {
                txt = transform.GetComponent<Text>();
                initStr = txt.text;
            }
            removeOtherTxt = initStr;
            removeGradientTxt = initStr;
            foreach (var pattern in m_patterts)
            {
                while (Regex.IsMatch(removeOtherTxt, pattern))
                {
                    GroupCollection groups = Regex.Match(removeOtherTxt, pattern).Groups;
                    removeOtherTxt = removeOtherTxt.Replace(groups[0].Value, groups[2].Value);
                }
            }

            string tempStr = removeOtherTxt;

            //需要重建end和start
            while (Regex.IsMatch(tempStr, @"<gradient=([\s\S]*?)>([\s\S]*?)</gradient>"))
            {
                Match match = Regex.Match(tempStr, @"<gradient=([\s\S]*?)>([\s\S]*?)</gradient>");
                int index = match.Groups[1].Index;
                int length = match.Groups[1].Length;
                int content_index = match.Groups[2].Index;
                int content_length = match.Groups[2].Length;
                int strindex = match.Groups[0].Index;
                int strlength = match.Groups[0].Length;
                string str = tempStr.Substring(index, length);
                string[] colorstrs = str.Split(',');
                string allstr = tempStr.Substring(strindex, strlength);
                string realstr = tempStr.Substring(content_index, content_length);
                ContentItem contentItem = new ContentItem() { startIndex = strindex, length = content_length };
                foreach (var colorstr in colorstrs)
                {
                    ColorUtility.TryParseHtmlString(colorstr, out var _color);
                    contentItem.gradientColor.Add(_color);
                }
                removeGradientTxt = removeGradientTxt.Replace(allstr, realstr);
                tempStr = tempStr.Replace(allstr, realstr);
                contentItems.Add(contentItem);
            }
            txt.text = removeGradientTxt;
        }

        protected override void OnValidate()
        {
            InitText();
        }

        [ContextMenu("ResetGradient")]
        public void ResetText()
        {
            txt.text = initStr;
        }

        protected Gradient()
        {

        }

        public static Color32 Multiply(Color32 a, Color32 b)
        {
            a.r = (byte)((a.r * b.r) >> 8);
            a.g = (byte)((a.g * b.g) >> 8);
            a.b = (byte)((a.b * b.b) >> 8);
            a.a = (byte)((a.a * b.a) >> 8);
            return a;
        }

        private void ModifyVertices(VertexHelper vh,int start,int end,List<Color> colors=null)
        {
            int step = 6;

            /*
               5-0 ---- 1
                | \    |
                |  \   |
                |   \  |
                |    \ |
                4-----3-2
            */
            //默认情况
            if (colors==null)
            {
                for (int n = start; n < end; n++)
                {
                    UIVertex tl = new UIVertex(), tr = new UIVertex(), bl = new UIVertex(), br = new UIVertex();
                    tl = verts[n * step + 0];
                    tr = verts[n * step + 1];
                    bl = verts[n * step + 4];
                    br = verts[n * step + 3];
                    vh.AddVert(tl);
                    vh.AddVert(tr);
                    vh.AddVert(br);

                    vh.AddVert(br);
                    vh.AddVert(bl);
                    vh.AddVert(tl);
                }
            }
            else if (colors.Count <= 1)
            {
                for (int n = start; n < end; n++)
                {
                    UIVertex tl = new UIVertex(), tr = new UIVertex(), bl = new UIVertex(), br = new UIVertex();
                    tl = verts[n * step + 0];
                    tr = verts[n * step + 1];
                    bl = verts[n * step + 4];
                    br = verts[n * step + 3];
                    tl.color = colors[0];
                    tr.color = colors[0];
                    bl.color = colors[0];
                    br.color = colors[0];
                    vh.AddVert(tl);
                    vh.AddVert(tr);
                    vh.AddVert(br);

                    vh.AddVert(br);
                    vh.AddVert(bl);
                    vh.AddVert(tl);
                }
            }
            else
            {
                int colorCount = colors.Count;
                for (int n = start; n < end; n++)
                {
                    UIVertex lastBottomR = default;
                    UIVertex lastbottomL = default;
                    for (int i = 0; i < colorCount - 1; i++)
                    {
                        UIVertex tl = new UIVertex(), tr = new UIVertex(), bl = new UIVertex(), br = new UIVertex();
                        if (i == 0)
                        {
                            tl = multiplyColor(verts[n * step + 0], colors[0]);
                            tr = multiplyColor(verts[n * step + 1], colors[0]);
                        }
                        else
                        {
                            tl = lastbottomL;
                            tr = lastBottomR;
                        }

                        if (i == colorCount - 2)
                        {
                            bl = multiplyColor(verts[n * step + 4], colors[colors.Count - 1]);
                            br = multiplyColor(verts[n * step + 3], colors[colors.Count - 1]);
                        }
                        else
                        {
                            bl = calcCenterVertex(tl, verts[n * step + 4], (colorCount - i - 2) * 1f / (colorCount - i - 1), colors[i + 1]);
                            br = calcCenterVertex(tr, verts[n * step + 3], (colorCount - i - 2) * 1f / (colorCount - i - 1), colors[i + 1]);
                        }

                        vh.AddVert(tl);
                        vh.AddVert(tr);
                        vh.AddVert(br);

                        vh.AddVert(br);
                        vh.AddVert(bl);
                        vh.AddVert(tl);
                        lastBottomR = br;
                        lastbottomL = bl;
                    }
                }
            }
            
        }

        private UIVertex multiplyColor(UIVertex vertex, Color color)
        {
            if (MultiplyTextColor)
                vertex.color = Multiply(vertex.color, color);
            else
                vertex.color = color;
            return vertex;
        }

        private UIVertex calcCenterVertex(UIVertex top, UIVertex bottom, float ratio, Color centerColor)
        {
            UIVertex center = new UIVertex();
            center.normal = (top.normal - bottom.normal) * ratio + bottom.normal;
            center.position = (top.position - bottom.position) * ratio + bottom.position;
            center.tangent = (top.tangent - bottom.tangent) * ratio + bottom.tangent;
            center.uv0 = (top.uv0 - bottom.uv0) * ratio + bottom.uv0;
            center.uv1 = (top.uv1 - bottom.uv1) * ratio + bottom.uv1;

            if (MultiplyTextColor)
            {
                var color = Color.Lerp(top.color, bottom.color, ratio);
                center.color = Multiply(color, centerColor);
            }
            else
            {
                center.color = centerColor;
            }

            return center;
        }

        #region implemented abstract members of BaseMeshEffect
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!this.IsActive()|| contentItems.Count == 0)
            {
                return;
            }
            verts = new List<UIVertex>(vh.currentVertCount);
            vh.GetUIVertexStream(verts);
            vh.Clear();
            //vh.GetUIVertexStream(_vertexBuffers);
            int pre = 0;

            for (int i = 0; i < contentItems.Count; i++)
            {
                int start = contentItems[i].startIndex;
                int end = start + contentItems[i].length;
                List<Color> colors = contentItems[i].gradientColor;
                ModifyVertices(vh, pre, start);
                ModifyVertices(vh, start, end, colors);
                pre = end;
                if (i == contentItems.Count - 1)
                {
                    ModifyVertices(vh, end, verts.Count / 6);
                }
            }

            for (int i = 0; i < vh.currentVertCount; i += 3)
            {
                vh.AddTriangle(i + 0, i + 1, i + 2);
            }
        }

        protected override void OnDestroy()
        {
            ResetText();
        }

        #endregion
    }

}