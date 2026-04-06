using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WhiteboardMarker : MonoBehaviour
{
    [SerializeField] private Transform _tip;
    [SerializeField] private int _penSize = 5;

    private Renderer _renderer;
    private Color[] _colours;
    private float _tipHeight;
    private RaycastHit _touch;
    private Whiteboard _whiteboard;
    private Vector2 _touchPos, _lastTouchPos;
    private bool _touchedLastFrame;
    private Quaternion _lastTouchRot;
    public Color markerColor = Color.green;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _colours = Enumerable.Repeat(markerColor, _penSize * _penSize).ToArray();
        _tipHeight = _tip.localScale.y;
    }

    void Update()
    {
        Draw();
    }

    [SerializeField][Range(0f, 1f)] private float _lerpRes = 0.05f; // Lower = smoother but slower
    private Vector2 _smoothedTouchPos;

    private void Draw()
    {
        if (Physics.Raycast(_tip.position, transform.up, out _touch, _tipHeight))
        {
            if (_touch.transform.CompareTag("Whiteboard"))
            {
                if (_whiteboard == null)
                {
                    _whiteboard = _touch.transform.GetComponent<Whiteboard>();
                }

                // 1. Get raw hit position
                Vector2 rawTouchPos = new Vector2(_touch.textureCoord.x, _touch.textureCoord.y);

                // 2. SMOOTHING: If this is the first touch, jump to it. Otherwise, slide toward it.
                if (!_touchedLastFrame)
                {
                    _smoothedTouchPos = rawTouchPos;
                }
                else
                {
                    // This "Lerps" the movement so micro-shakes are filtered out
                    _smoothedTouchPos = Vector2.Lerp(_smoothedTouchPos, rawTouchPos, 0.2f);
                }

                // 3. Convert to Texture Coordinates
                var x = (int)(_smoothedTouchPos.x * _whiteboard.textureSize.x - (_penSize / 2));
                var y = (int)(_smoothedTouchPos.y * _whiteboard.textureSize.y - (_penSize / 2));

                // Bounds check
                if (y < 0 || y > _whiteboard.textureSize.y || x < 0 || x > _whiteboard.textureSize.x) return;

                if (_touchedLastFrame)
                {
                    // 4. INTERPOLATION: Draw a line between last frame and this frame
                    for (float f = 0; f <= 1.00f; f += _lerpRes)
                    {
                        var lerpX = (int)Mathf.Lerp(_lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(_lastTouchPos.y, y, f);
                        _whiteboard.texture.SetPixels(lerpX, lerpY, _penSize, _penSize, _colours);
                    }

                    _whiteboard.texture.Apply();
                }

                _lastTouchPos = new Vector2(x, y);
                _touchedLastFrame = true;
                return;
            }
        }

        _whiteboard = null;
        _touchedLastFrame = false;
    }
}
