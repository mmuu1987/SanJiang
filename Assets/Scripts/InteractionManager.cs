using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Video;

public class InteractionManager : MonoBehaviour, IEndDragHandler, IBeginDragHandler, IDragHandler, IPointerClickHandler
{

    public VideoPlayer StandByVideoPlayer;

    public Button 滨江邻里Button;

    public Button 滨江健康Button;

    public Button 滨江智慧Button;



    public Transform 滨江邻里Parent;

    public Transform 滨江健康Parent;


    public Transform 滨江智慧Parent;




    public List<Button> MainBtns = new List<Button>();

    private List<Transform> _dragTransforms = new List<Transform>();


    private float _standbyTime;

    private float StandbyMaxTime = 50f;


    /// <summary>
    /// 是否是待机
    /// </summary>
    private bool _isStandBy = true;


    private Coroutine _autoCoroutine;

    /// <summary>
    /// x轴拖动的距离
    /// </summary>
    private float _xDis;


    /// <summary>
    /// 当前激活的页面
    /// </summary>
    private Transform _curActiveTf;


    private Tween _tween;

    // Start is called before the first frame update  
    void Start()
    {

        Screen.SetResolution(6480, 1920, true);

        StandByVideoPlayer.url = Application.streamingAssetsPath + "/StandBy.mp4";

        StandByVideoPlayer.Play();

        滨江邻里Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(滨江邻里Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(滨江邻里Button.transform.parent, _curActiveTf);

            

            HandleMainBtnVisual("城市邻里中心");

        }));


        滨江健康Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(滨江健康Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(滨江健康Button.transform.parent,_curActiveTf);

           

            HandleMainBtnVisual("健康互助公园");

        }));

        滨江智慧Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(滨江智慧Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(滨江智慧Button.transform.parent,_curActiveTf);

           

            HandleMainBtnVisual("滨江智慧");
        }));



        HandleAlpha(滨江邻里Parent);
        HandleAlpha(滨江健康Parent);
        HandleAlpha(滨江智慧Parent);

        SetOnClick(滨江邻里Parent);
        SetOnClick(滨江健康Parent);
        SetOnClick(滨江智慧Parent);


        SetBtnOnClick(滨江智慧Parent.Find("智能家居"));


        HandleMainBtnEvent();

        ActiveStandby(false);

    }

    /// <summary>
    /// 处理主菜单的界面视觉效果
    /// </summary>
    private void HandleMainBtnVisual(string Btnname)
    {
        Image isActive = null;//父物体是否激活，激活后就不在判断

        MainBtns[0].transform.parent.gameObject.SetActive(true);
        foreach (var item in MainBtns)
        {
            Image imageParent = item.transform.parent.GetComponent<Image>();

            if (imageParent == null) continue;//父级别不做逻辑流程

            Image imageChild = item.GetComponent<Image>();

            if (item.name == Btnname)
            {
                imageChild.color = Color.white;
                if (imageParent != null)
                {
                    imageParent.color = Color.white;
                    isActive = imageParent;
                }
            }
            else
            {
                if (imageParent != null && imageParent != isActive)
                {
                    imageParent.color = new Color(1, 1, 1, 0);

                }

                imageChild.color = new Color(1, 1, 1, 0);
            }
        }
    }

   
    /// <summary>
    /// 处理主菜单的点击事件
    /// </summary>
    private void HandleMainBtnEvent()
    {
        foreach (var item in MainBtns)
        {
            if (item.name == "Left")
            {
                item.onClick.AddListener(() => {

                    
                    Previous();
                });
                
            }
            else if(item.name=="Right")
            {
               
                item.onClick.AddListener(() => {
                   
                    Next();

                });
            }
            else if (item.name == "Back")
            {
                
                item.onClick.AddListener(() => {

                    Back();
                });
            }
            else if (item.name == "滨江健康")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent("健康互助公园", 滨江健康Parent);
                    HandleMainBtnVisual("健康互助公园");
                });
               
            }
            else if (item.name == "健康互助公园")
            {

                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent(item.name, 滨江健康Parent);
                    HandleMainBtnVisual("健康互助公园");
                });
              
            }
            else if (item.name == "和趣全龄健康运动系统")
            {

                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, 滨江健康Parent);
                    HandleMainBtnVisual("和趣全龄健康运动系统");
                });
                  
            }

            else if (item.name == "滨江邻里")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent("城市邻里中心", 滨江邻里Parent);
                    HandleMainBtnVisual("城市邻里中心");

                });
            }

            else if (item.name == "组团邻里中心")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, 滨江邻里Parent);
                    HandleMainBtnVisual("组团邻里中心");
                });
            }

            else if (item.name == "社区邻里中心")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, 滨江邻里Parent);
                    HandleMainBtnVisual("社区邻里中心");
                });
            }

            else if (item.name == "城市邻里中心")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, 滨江邻里Parent);
                    HandleMainBtnVisual("城市邻里中心");
                });
            }

            else if (item.name == "滨江智慧")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    滨江智慧Button.onClick.Invoke();
                    滨江健康Parent.gameObject.SetActive(false);
                    滨江邻里Parent.gameObject.SetActive(false);
                    滨江智慧Parent.gameObject.SetActive(true);
                });
            }

        }
    }

    private void HandleEvent(string btnName,Transform parent)
    {
        _dragTransforms = GetChild(parent);


        Transform oldTf = _curActiveTf;

        foreach (var tf in _dragTransforms)
        {
            if (tf.name == btnName)
            {
                _curActiveTf = tf;
            }
        }

        if (oldTf == _curActiveTf) return;

        Enter(oldTf, _curActiveTf);

        EnterEvent(_curActiveTf);
    }
    /// <summary>
    /// 初始化透明度  
    /// </summary>
    private void HandleAlpha(Transform parent)
    {
        MaskableGraphic[] enterMg = parent.GetComponentsInChildren<MaskableGraphic>(true);
        foreach (MaskableGraphic graphic in enterMg)
        {
            graphic.DOFade(0f, 0f);
        }
    }

    public List<Transform> GetChild(Transform parent)
    {
        Transform[] temps = parent.GetComponentsInChildren<Transform>(true);

        List<Transform> childs = new List<Transform>();


        foreach (var item in temps)
        {
            if (item.parent == parent && item.name != "智能家居")//智能家居为特殊情况 
            {
                childs.Add(item);

            }
        }

        return childs;
    }

    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    private void SetBtnOnClick(Transform parent)
    {
        Button[] btns = parent.GetComponentsInChildren<Button>(true);

        foreach (var item in btns)
        {
            item.onClick.AddListener(() =>
            {

               
                //if (_tween != null)
                //{
                //    Debug.Log(_curIndex + "   _tween  停止");
                //    return;
                //}

                if (item.name == "智能家居")//如果是智能家居按钮
                {

                    Transform temp = 滨江智慧Parent.Find("智能家居");


                    Enter(item.transform.parent, temp);

                    EnterEvent(temp);

                    _curActiveTf = temp;

                }
                else
                {
                    _curIndex = item.GetComponent<RectTransform>().GetSiblingIndex();

                    Debug.Log(_curIndex);

                    AutoEvent(parent);

                    if (_autoCoroutine != null) StopCoroutine(_autoCoroutine);
                    _autoCoroutine = StartCoroutine(AutoPlay(parent));
                }

            });

            RectTransform rt = item.GetComponent<RectTransform>();

            rt.DOKill();
            //设置动画 
            rt.DOLocalMoveY(rt.anchoredPosition.y + 20, 3.5f).SetLoops(-1, LoopType.Yoyo).SetDelay(UnityEngine.Random.Range(0f, 7f));
        }
    }

    private void SetOnClick(Transform parent)
    {
        List<Transform> parents = GetChild(parent);
        foreach (var item in parents)
        {
            SetBtnOnClick(item);
        }
    }
    private int _curIndex = -1;

    /// <summary>
    /// 自动播放    538
    /// </summary>
    /// <param name="parent"></param>
    private void AutoEvent(Transform parent)
    {
        Button[] buttons = parent.GetComponentsInChildren<Button>();


        if (_curIndex > buttons.Length - 1) _curIndex = 0;

        foreach (Button button in buttons)
        {
            RectTransform rtf = button.GetComponent<RectTransform>();
            int index = rtf.GetSiblingIndex();


            Transform[] temps = button.GetComponentsInChildren<Transform>();

            if (index == _curIndex)
            {

                if(rtf.parent.name!="智能家居")
                {
                    rtf.DOSizeDelta(new Vector2(538f, 119f), 0.05f).OnComplete(() => {

                        foreach (var item in temps)
                        {
                            if (item == button.transform) continue;

                            if (item.name.Contains("Descripts"))
                            {
                                item.DOScale(Vector3.one, 0.35f);
                            }
                        }

                    });
                }
                else
                {
                    rtf.DOSizeDelta(new Vector2(393f, 410f), 0.03f).OnComplete(() => {

                        foreach (var item in temps)
                        {
                            if (item == button.transform) continue;

                            if (item.name.Contains("Descripts"))
                            {
                                item.DOScale(Vector3.one, 0.35f);
                            }
                        }

                    });
                }

               
            }
            else
            {

                foreach (var item in temps)
                {
                    if (item == button.transform)//按钮本身  
                    {
                        if(item.transform.parent.name!="智能家居")
                        {
                            rtf.DOSizeDelta(new Vector2(538f, 119f), 0.05f);
                        }
                        
                    };

                    if (item.name.Contains("Descripts"))
                    {
                        item.DOScale(Vector3.zero, 0.35f);
                    }
                }
            }
        }
        _curIndex++;


    }
    private IEnumerator AutoPlay(Transform parent)
    {

        while (true)
        {
            yield return new WaitForSeconds(3f);
            //Debug.Log("自动播放");
            AutoEvent(parent);
        }



    }

    /// <summary>
    /// 进入该菜单自动出发
    /// </summary>
    /// <param name="parent"></param>
    private void EnterEvent(Transform parent)
    {
        Button[] btns = parent.GetComponentsInChildren<Button>();
        Debug.Log("执行第一个点击");
        if (btns.Length > 0)
        {
            btns[0].onClick.Invoke();//进来就唤醒第一个
        }
    }
    /// <summary>
    /// 进入子菜单,视觉效果
    /// </summary>
    private void Enter(Transform exit, Transform enter, bool isenterParentActive = true, Action action = null)
    {

        if (exit != null)
        {
            MaskableGraphic[] exitMg = exit.GetComponentsInChildren<MaskableGraphic>();


            foreach (MaskableGraphic graphic in exitMg)
            {
                if (graphic.transform == exit)
                {
                    _tween = graphic.DOFade(0f, 0.5f).OnComplete((() =>
                     {
                         graphic.gameObject.SetActive(false);
                         _tween = null;
                         if (action != null) action();
                        //Debug.Log("清空 _tween  exit");
                      
                     }));//动画完成的事件
                }
                else
                {
                    graphic.DOKill();
                    graphic.DOFade(0f, 0.35f); 
                }
            }
        }


        if (enter == null) return;

        if (isenterParentActive)
        {
            if (enter.parent != null) enter.parent.gameObject.SetActive(true);
        }

        enter.gameObject.SetActive(true);

        MaskableGraphic[] enterMsg = enter.GetComponentsInChildren<MaskableGraphic>();


        foreach (MaskableGraphic graphic in enterMsg)
        {
            DOTweenAnimation da = graphic.GetComponent<DOTweenAnimation>();//有动画组件就紧掉，特指智能家居部分

            if ( graphic.transform.parent.name=="智能家居")
            {
                graphic.color = new Color(0f, 0f, 0f, 1f);
                graphic.DOColor(Color.white, 1.25f).SetDelay(UnityEngine.Random.Range(0f, 3f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
                Debug.Log("开始");
            }
            else
            {
                _tween = graphic.DOFade(1f, 0.5f).OnComplete(() => {

                    _tween = null;
                    //Debug.Log("清空 _tween enter");
                });
            }
          
        }

        if(enter.name=="健康互助公园")
        {
            if (_autoQueueShow != null) StopCoroutine(_autoQueueShow);
              _autoQueueShow = StartCoroutine(QueueShow(enter));
        }

    }

    private Coroutine _autoQueueShow;
    /// <summary>
    /// 健康互助公园的按顺序显示
    /// </summary>
    private IEnumerator QueueShow(Transform parent)
    {
        RectTransform[] tfs = parent.GetComponentsInChildren<RectTransform>();

        List<RectTransform> childs = new List<RectTransform>();

        foreach (var item in tfs)
        {
            if (item.transform != parent)
            {
                if (!item.name.Contains("title"))
                {
                    item.sizeDelta = new Vector2(0f, 81f);

                    childs.Add(item);
                }

            }
        }

        int index = 0;

        while (true)
        {
           

            int temp = 0;
            foreach (var item in childs)
            {
                if (temp <= index)
                {
                    item.DOSizeDelta( new Vector2(337f, 81f),0.35f);
                    temp++;
                }
            }
            index++;
            Debug.Log(index+"  temp "+ temp);
            if (index >= childs.Count)
            {
                foreach (var item in childs)
                {
                    item.DOKill();
                    item.DOSizeDelta(new Vector2(0f, 81f), 0.1f);
                }
                Debug.Log("重置");
                index = 0;
                temp = 0;
            }

            yield return new WaitForSeconds(1.5f);
        }

    }
    // Update is called once per frame
    void Update()
    {


        if (!_isStandBy)
        {
            this._standbyTime += Time.deltaTime;
            if (this._standbyTime >= this.StandbyMaxTime)
            {
                _isStandBy = true;
                this.ActiveStandby(false);
                Back(true);
            }
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.touchCount > 0))
        {
            if (_isStandBy)
            {
                this.ActiveStandby(true);
                _isStandBy = false;
            }

            _standbyTime = 0f;
        }
    }


    /// <summary>
    /// 是否从待机中恢复   
    /// </summary>
    /// <param name="isStandBy">true为不待机，false为待机</param>
    private void ActiveStandby(bool isStandBy)
    {
        if (isStandBy)
        {
            StandByVideoPlayer.Stop();
            StandByVideoPlayer.gameObject.SetActive(false);

        }
        else
        {

            StandByVideoPlayer.gameObject.SetActive(true);
            StandByVideoPlayer.Play();
        }

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        _xDis = eventData.position.x - _xDis;

        float absX = Mathf.Abs(_xDis);

        // Debug.Log("拖动的距离是 " + absX); 
        if (absX >= 100)
        {
            if (_xDis > 0)
            {
                Previous();
            }
            else
            {
                Next();
            }
        }
    }
    private void Next()
    {
        if (_curActiveTf == null ||_curActiveTf.name== "智能家居") return;

        if (_tween != null) return;

        int nextIndex = _dragTransforms.IndexOf(_curActiveTf);

        nextIndex++;
        if (_dragTransforms.Count - 1 < nextIndex)
        {
            nextIndex = 0;
        }

        Transform nextTransform = _dragTransforms[nextIndex];

        
        if (nextTransform == _curActiveTf) return;
       
        Enter(_curActiveTf, nextTransform);

        _curActiveTf = nextTransform;
        EnterEvent(_curActiveTf);
        //Debug.Log(_curActiveTf.name);
        HandleMainBtnVisual(_curActiveTf.name);
    }

    private void Previous()
    {
        if (_curActiveTf == null || _curActiveTf.name == "智能家居") return;

        if (_tween != null) return;

        int nextIndex = _dragTransforms.IndexOf(_curActiveTf);

        nextIndex--;

        if (nextIndex < 0) nextIndex = _dragTransforms.Count - 1;

        Transform nextTransform = _dragTransforms[nextIndex];

        

        if (nextTransform == _curActiveTf) return;

       
        Enter(_curActiveTf, nextTransform);

        _curActiveTf = nextTransform;
        EnterEvent(_curActiveTf);
        //Debug.Log(_curActiveTf.name);
        HandleMainBtnVisual(_curActiveTf.name);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        _xDis = eventData.position.x;

        //Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {

    }
    /// <summary>
    /// 是否直接返回待机
    /// </summary>
    /// <param name="isStandBy"></param>
    public void Back(bool isStandBy = false)
    {
        if (_tween != null) return;

        if (_curActiveTf!=null &&_curActiveTf.name == "智能家居")
        {

            _dragTransforms = GetChild(滨江智慧Parent);


            Transform oldTf = _curActiveTf;

            foreach (var tf in _dragTransforms)
            {
                if (tf.name == "滨江智慧场景")
                {
                    _curActiveTf = tf;
                }
            }

            if (oldTf == _curActiveTf) return;

            Enter(oldTf, _curActiveTf,true,()=> {

                //HandleMainBtnVisual("滨江智慧");
                if (isStandBy)
                {
                    Back();
                }

            });
            EnterEvent(_curActiveTf);
        }
        else
        {
            Enter(_curActiveTf, 滨江健康Button.transform.parent, true, () =>
            {
                Debug.Log("隐藏父物体");
                _curActiveTf.transform.parent.gameObject.SetActive(false);
                _curActiveTf = null;
            });
            MainBtns[0].transform.parent.gameObject.SetActive(false);
        }


    }
   
    private float firstClicked = 0;
    private float secondClicked = 0;
    public float Interval = 0.25f;//规定时间点击两次则会双击

    private Vector2 firstClickPos;

    private Vector2 secondClickPos;
    public void OnPointerClick(PointerEventData eventData)
    {
        secondClicked = Time.realtimeSinceStartup;

        secondClickPos = eventData.position;

        if (secondClicked - firstClicked < Interval)
        {
            float dis = Vector3.Distance(firstClickPos, secondClickPos);

            if(dis<50f)
            {
                //双击
                Debug.Log("双击" +dis);
                Back();
            }
           
        }
        else
        {
            firstClicked = secondClicked;
            firstClickPos = secondClickPos;
        }
    }
}
