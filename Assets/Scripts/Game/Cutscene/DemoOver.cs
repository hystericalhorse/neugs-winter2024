using System.Collections;
using UnityEngine;

public class DemoOver : MonoBehaviour
{
    [SerializeField] GameObject DemoOverScreen;

    public void DemoScreenPopup()
    {
        PlayerManager.instance.TogglePlayerController(false);

        DemoOverScreen = UIManager.instance.DemoOverScreen;
        AudioManager.instance.PlaySound("DemoJumpscare");
		FindAnyObjectByType<TransitionScreen>().SetVisible(true);

		StartCoroutine(FadeIn());
    }

    public IEnumerator FadeIn()
    {
        DemoOverScreen.SetActive(true);
		var g = DemoOverScreen.GetComponent<CanvasGroup>();
		g.alpha = 0.0f;

        yield return new WaitForSeconds(1.0f);

        while (g.alpha < 1.0f)
        {
            g.alpha += Time.deltaTime * 4f;
            yield return null;
        }
	}
}
