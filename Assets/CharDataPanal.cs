using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharDataPanal : MonoBehaviour
{
    [SerializeField] TextMesh Name, Partner, Shelter;

    public static CharDataPanal I;

    private void Start()
    {
        I = this;
    }

    Human CurHu;
    public void View(Human human)
    {
        //transform.SetParent(human.transform);
        CurHu = human;

        Name.text = human.Name;
        Partner.text = human.Partner == null ? "Single" : human.Partner.Name;
        Shelter.text = human.Shelter == null ? "Homeless" : human.Shelter.transform.position.ToString();

        if (ViewCo != null) StopCoroutine(ViewCo);
        ViewCo = StartCoroutine(SyncPoz());
    }

    Coroutine ViewCo;
    IEnumerator SyncPoz()
    {
        while (true)
        {
            if (CurHu == null)
            {
                transform.position = Vector3.one * 999; //2343
                yield break;
            }
            transform.position = CurHu.transform.position + (CurHu.transform.up * 5);
            transform.LookAt(Camera.main.transform.position);
            yield return new WaitForSeconds(.01f);
        }
    }
}
