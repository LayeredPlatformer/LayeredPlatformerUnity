using UnityEngine;
using System.Collections;
using System.Linq;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlatformCharacter3D))]
public class Platformer3DUserControl : MonoBehaviour
{
    private const int MaxLayer = 2;
    public const float LayerDifference = 3;

    private PlatformCharacter3D m_Character;
    private bool m_Jump;

    private bool _layerToggle = false;

    private int _currentLayer = 0;


    private void Awake()
    {
        m_Character = GetComponent<PlatformCharacter3D>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            // Read the jump input in Update so button presses aren't missed.
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        if (!_layerToggle)
        {
            _layerToggle = Input.GetKeyDown(KeyCode.F);
        }
    }


    private void FixedUpdate()
    {
        // Read the inputs.
        bool crouch = Input.GetKey(KeyCode.LeftControl);
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        // Pass all parameters to the character control script.
        m_Character.Move(h, crouch, m_Jump);
        m_Jump = false;

        if (_layerToggle)
        {
            _currentLayer++;
            transform.Translate(0, 0, LayerDifference);

            if (_currentLayer > MaxLayer)
            {
                transform.Translate(0, 0, -1 * (LayerDifference * (MaxLayer + 1)));
                _currentLayer = 0;
            }

            var colorLayers = GameObject.FindGameObjectsWithTag("ColorLayer").ToList();

            for (int i = 0; i < colorLayers.Count; i++)
            {
                if (i < _currentLayer)
                {
                    SetGameObjectChildrenOpacity(colorLayers[i], 0.5f);
                }
                else
                {
                    SetGameObjectChildrenOpacity(colorLayers[i], 1f);
                }
            }

            _layerToggle = false;
        }
    }

    private static void SetGameObjectChildrenOpacity(GameObject gameObject, float opacity)
    {
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            Color layerColor = renderer.material.color;
            layerColor.a = opacity;
            renderer.material.color = layerColor;
        }
    }
}
