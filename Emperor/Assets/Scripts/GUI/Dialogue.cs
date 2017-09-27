using UnityEngine;

public class Dialogue : MonoBehaviour {

    private bool _talk = false;
    private Vector3 _textPosition;
    private GUIStyle _style;
	private GUIContent _content;

    public int FontSize = 16;

	private const float DIALOGUE_LINE_DURATION = 3.0f;
	public float TimeBeforeTextDisappearance = DIALOGUE_LINE_DURATION;

	public string[] DialogueLines;

	private int _sentenceIndex;


	void Start()
	{
		Texture2D background = new Texture2D(1, 1, TextureFormat.ARGB32, false);
		background.SetPixel(0, 0, new Color(0.9f, 0.9f, 0.9f, 1f));
		background.Apply();

		_style = new GUIStyle();
        _style.fontSize = FontSize;
		_style.normal.background = background;
		_style.fixedHeight = 0;
		_style.fixedWidth = 0;

		_sentenceIndex = 0;

		_content = new GUIContent(DialogueLines[_sentenceIndex], background); 
	}

    void Update()
    {
        if (_talk)
        {
            TimeBeforeTextDisappearance -= Time.deltaTime;
			if (TimeBeforeTextDisappearance < 0.0f)
            {
				SelectNextDialogueLine();
				TimeBeforeTextDisappearance = DIALOGUE_LINE_DURATION;
            }
        }
        else
        {
            TimeBeforeTextDisappearance = 5.0f;
        }
    }

    void OnGUI()
    {
        if (_talk)
        {
            _textPosition = transform.position + 0.5f * transform.gameObject.GetComponent<SpriteRenderer>().bounds.size;
            Vector3 getPixelPos = Camera.main.WorldToScreenPoint(_textPosition);
            getPixelPos.y = Screen.height - getPixelPos.y;
			
			GUI.Label(new Rect(getPixelPos.x, getPixelPos.y, 150, 20), _content, _style);
			
        }
    }
    
    public void SetCanTalk(bool talk)
    {
        _talk = talk;
    }

	private void SelectNextDialogueLine()
	{
		_sentenceIndex++;

		if(_sentenceIndex >= DialogueLines.Length)
		{
			_sentenceIndex = 0;
			_talk = false;
		}

		_content.text = DialogueLines[_sentenceIndex];
	}
}
