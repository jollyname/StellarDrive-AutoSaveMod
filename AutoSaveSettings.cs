using System.Collections.Generic;
using TMPro;
using UI.Common.Options;
using UnityEngine;
using UnityEngine.UI;

namespace AutoSaveMod
{
    public class AutoSaveSettings : MonoBehaviour
    {
        private static readonly int[] Delays =
        {
            5,
            10,
            20,
            30,
            45,
            60
        };

        void Start()
        {
            // AutoSave toggle
            Toggle _toggle = CreateToggle("AutoSaveToggle", "Autosave");

            _toggle.isOn = AutoSaveMod.autosaveEnabled.Value;
            _toggle.onValueChanged.AddListener(OnToggleChanged);

            // SaveDelay dropdown
            TMP_Dropdown _dropdown = CreateDropdown("AutoSaveDelay", "Autosave Delay");

            _dropdown.ClearOptions();

            _dropdown.AddOptions(new List<string>
            {
                "5 minutes",
                "10 minutes",
                "20 minutes",
                "30 minutes",
                "45 minutes",
                "60 minutes"
            });

            int index = System.Array.IndexOf(Delays, AutoSaveMod.autosaveDelay.Value);

            if (index < 0) index = 2;

            _dropdown.value = index;
            _dropdown.RefreshShownValue();

            _dropdown.onValueChanged.AddListener(value =>
            {
                AutoSaveMod.autosaveDelay.Value = Delays[value];
                AutoSaveMod.ResetTimer();
            });
        }

        Toggle CreateToggle(string _name, string _labelContent)
        {
            Transform existingToggle = transform.Find(_name);

            Toggle _toggle;
            TMP_Text _label;

            if (existingToggle != null)
            {
                _toggle = existingToggle.GetComponentInChildren<Toggle>();
                _label = existingToggle.GetComponentInChildren<TMP_Text>();
            }
            else
            {
                Transform devTools = transform.Find("DevTools");

                GameObject obj = Instantiate(devTools.gameObject, transform);
                Destroy(obj.GetComponent<DevToolSettings>());

                obj.name = _name;

                _toggle = obj.GetComponentInChildren<Toggle>();
                _label = obj.GetComponentInChildren<TMP_Text>();

                _toggle.onValueChanged.RemoveAllListeners();
            }

            _label.text = _labelContent;

            return _toggle;
        }

        TMP_Dropdown CreateDropdown(string _name, string _labelContent)
        {
            Transform existingDropdown = transform.Find(_name);

            TMP_Dropdown dropdown;
            TMP_Text label;

            if (existingDropdown != null)
            {
                dropdown = existingDropdown.GetComponentInChildren<TMP_Dropdown>();
                label = existingDropdown.GetComponentInChildren<TMP_Text>();
            }
            else
            {
                Transform windowMode = transform.Find("WindowMode");

                GameObject obj = Instantiate(windowMode.gameObject, transform);
                obj.name = _name;

                dropdown = obj.GetComponentInChildren<TMP_Dropdown>();
                label = obj.GetComponentInChildren<TMP_Text>();

                dropdown.onValueChanged.RemoveAllListeners();
            }

            label.text = _labelContent;

            return dropdown;
        }

        private void OnToggleChanged(bool value)
        {
            AutoSaveMod.autosaveEnabled.Value = value;

            if (value) AutoSaveMod.ResetTimer();
        }
    }
}