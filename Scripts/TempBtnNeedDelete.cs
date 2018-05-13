using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempBtnNeedDelete : MonoBehaviour {
    RecordData rd = new RecordData();
    RefreshUI rui = new RefreshUI();
    // Use this for initialization
    void Start()
    {

        transform.GetComponent<Button>().onClick.AddListener(() =>
        {
            rd.DataEvent += rui.RefreshMainGold;

        CreateANewVenture.Instance.newRecordData.Gold -= 5;
        CreateANewVenture.Instance.newRecordData.Health -= 5;

        rd.UpdateData();
        rd.DataEvent -= rui.RefreshMainGold;

        });
    }
    private void OnDestroy()
    {
        rd.DataEvent -= rui.RefreshMainGold;
    }
}
