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

    public Button ��������Button;

    public Button ��������Button;

    public Button �����ǻ�Button;



    public Transform ��������Parent;

    public Transform ��������Parent;


    public Transform �����ǻ�Parent;




    public List<Button> MainBtns = new List<Button>();

    private List<Transform> _dragTransforms = new List<Transform>();


    private float _standbyTime;

    private float StandbyMaxTime = 50f;


    /// <summary>
    /// �Ƿ��Ǵ���
    /// </summary>
    private bool _isStandBy = true;


    private Coroutine _autoCoroutine;

    /// <summary>
    /// x���϶��ľ���
    /// </summary>
    private float _xDis;


    /// <summary>
    /// ��ǰ�����ҳ��
    /// </summary>
    private Transform _curActiveTf;


    private Tween _tween;

    // Start is called before the first frame update  
    void Start()
    {

        Screen.SetResolution(6480, 1920, true);

        StandByVideoPlayer.url = Application.streamingAssetsPath + "/StandBy.mp4";

        StandByVideoPlayer.Play();

        ��������Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(��������Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(��������Button.transform.parent, _curActiveTf);

            

            HandleMainBtnVisual("������������");

        }));


        ��������Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(��������Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(��������Button.transform.parent,_curActiveTf);

           

            HandleMainBtnVisual("����������԰");

        }));

        �����ǻ�Button.onClick.AddListener((() =>
        {
            if (_tween != null) return;
            _dragTransforms = GetChild(�����ǻ�Parent);

            _curActiveTf = _dragTransforms[0];


            EnterEvent(_curActiveTf);

            Enter(�����ǻ�Button.transform.parent,_curActiveTf);

           

            HandleMainBtnVisual("�����ǻ�");
        }));



        HandleAlpha(��������Parent);
        HandleAlpha(��������Parent);
        HandleAlpha(�����ǻ�Parent);

        SetOnClick(��������Parent);
        SetOnClick(��������Parent);
        SetOnClick(�����ǻ�Parent);


        SetBtnOnClick(�����ǻ�Parent.Find("���ܼҾ�"));


        HandleMainBtnEvent();

        ActiveStandby(false);

    }

    /// <summary>
    /// �������˵��Ľ����Ӿ�Ч��
    /// </summary>
    private void HandleMainBtnVisual(string Btnname)
    {
        Image isActive = null;//�������Ƿ񼤻�����Ͳ����ж�

        MainBtns[0].transform.parent.gameObject.SetActive(true);
        foreach (var item in MainBtns)
        {
            Image imageParent = item.transform.parent.GetComponent<Image>();

            if (imageParent == null) continue;//���������߼�����

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
    /// �������˵��ĵ���¼�
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
            else if (item.name == "��������")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent("����������԰", ��������Parent);
                    HandleMainBtnVisual("����������԰");
                });
               
            }
            else if (item.name == "����������԰")
            {

                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent(item.name, ��������Parent);
                    HandleMainBtnVisual("����������԰");
                });
              
            }
            else if (item.name == "��Ȥȫ�佡���˶�ϵͳ")
            {

                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, ��������Parent);
                    HandleMainBtnVisual("��Ȥȫ�佡���˶�ϵͳ");
                });
                  
            }

            else if (item.name == "��������")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    HandleEvent("������������", ��������Parent);
                    HandleMainBtnVisual("������������");

                });
            }

            else if (item.name == "������������")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, ��������Parent);
                    HandleMainBtnVisual("������������");
                });
            }

            else if (item.name == "������������")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, ��������Parent);
                    HandleMainBtnVisual("������������");
                });
            }

            else if (item.name == "������������")
            {
                item.onClick.AddListener(() =>
                {
                    if (_tween != null) return;
                    HandleEvent(item.name, ��������Parent);
                    HandleMainBtnVisual("������������");
                });
            }

            else if (item.name == "�����ǻ�")
            {
                item.onClick.AddListener(() => {
                    if (_tween != null) return;
                    �����ǻ�Button.onClick.Invoke();
                    ��������Parent.gameObject.SetActive(false);
                    ��������Parent.gameObject.SetActive(false);
                    �����ǻ�Parent.gameObject.SetActive(true);
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
    /// ��ʼ��͸����  
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
            if (item.parent == parent && item.name != "���ܼҾ�")//���ܼҾ�Ϊ������� 
            {
                childs.Add(item);

            }
        }

        return childs;
    }

    /// <summary>
    /// ���ð�ť����¼�
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
                //    Debug.Log(_curIndex + "   _tween  ֹͣ");
                //    return;
                //}

                if (item.name == "���ܼҾ�")//��������ܼҾӰ�ť
                {

                    Transform temp = �����ǻ�Parent.Find("���ܼҾ�");


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
            //���ö��� 
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
    /// �Զ�����    538
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

                if(rtf.parent.name!="���ܼҾ�")
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
                    if (item == button.transform)//��ť����  
                    {
                        if(item.transform.parent.name!="���ܼҾ�")
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
            //Debug.Log("�Զ�����");
            AutoEvent(parent);
        }



    }

    /// <summary>
    /// ����ò˵��Զ�����
    /// </summary>
    /// <param name="parent"></param>
    private void EnterEvent(Transform parent)
    {
        Button[] btns = parent.GetComponentsInChildren<Button>();
        Debug.Log("ִ�е�һ�����");
        if (btns.Length > 0)
        {
            btns[0].onClick.Invoke();//�����ͻ��ѵ�һ��
        }
    }
    /// <summary>
    /// �����Ӳ˵�,�Ӿ�Ч��
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
                        //Debug.Log("��� _tween  exit");
                      
                     }));//������ɵ��¼�
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
            DOTweenAnimation da = graphic.GetComponent<DOTweenAnimation>();//�ж�������ͽ�������ָ���ܼҾӲ���

            if ( graphic.transform.parent.name=="���ܼҾ�")
            {
                graphic.color = new Color(0f, 0f, 0f, 1f);
                graphic.DOColor(Color.white, 1.25f).SetDelay(UnityEngine.Random.Range(0f, 3f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutQuad);
                Debug.Log("��ʼ");
            }
            else
            {
                _tween = graphic.DOFade(1f, 0.5f).OnComplete(() => {

                    _tween = null;
                    //Debug.Log("��� _tween enter");
                });
            }
          
        }

        if(enter.name=="����������԰")
        {
            if (_autoQueueShow != null) StopCoroutine(_autoQueueShow);
              _autoQueueShow = StartCoroutine(QueueShow(enter));
        }

    }

    private Coroutine _autoQueueShow;
    /// <summary>
    /// ����������԰�İ�˳����ʾ
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
                Debug.Log("����");
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
    /// �Ƿ�Ӵ����лָ�   
    /// </summary>
    /// <param name="isStandBy">trueΪ��������falseΪ����</param>
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

        // Debug.Log("�϶��ľ����� " + absX); 
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
        if (_curActiveTf == null ||_curActiveTf.name== "���ܼҾ�") return;

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
        if (_curActiveTf == null || _curActiveTf.name == "���ܼҾ�") return;

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
    /// �Ƿ�ֱ�ӷ��ش���
    /// </summary>
    /// <param name="isStandBy"></param>
    public void Back(bool isStandBy = false)
    {
        if (_tween != null) return;

        if (_curActiveTf!=null &&_curActiveTf.name == "���ܼҾ�")
        {

            _dragTransforms = GetChild(�����ǻ�Parent);


            Transform oldTf = _curActiveTf;

            foreach (var tf in _dragTransforms)
            {
                if (tf.name == "�����ǻ۳���")
                {
                    _curActiveTf = tf;
                }
            }

            if (oldTf == _curActiveTf) return;

            Enter(oldTf, _curActiveTf,true,()=> {

                //HandleMainBtnVisual("�����ǻ�");
                if (isStandBy)
                {
                    Back();
                }

            });
            EnterEvent(_curActiveTf);
        }
        else
        {
            Enter(_curActiveTf, ��������Button.transform.parent, true, () =>
            {
                Debug.Log("���ظ�����");
                _curActiveTf.transform.parent.gameObject.SetActive(false);
                _curActiveTf = null;
            });
            MainBtns[0].transform.parent.gameObject.SetActive(false);
        }


    }
   
    private float firstClicked = 0;
    private float secondClicked = 0;
    public float Interval = 0.25f;//�涨ʱ�����������˫��

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
                //˫��
                Debug.Log("˫��" +dis);
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
