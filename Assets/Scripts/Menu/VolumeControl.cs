using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;  // Kéo thả slider từ inspector vào đây
    public AudioSource audioSource;  // Kéo thả audio source từ inspector vào đây

    void Start()
    {
        // Đặt giá trị ban đầu cho slider bằng với âm lượng hiện tại của audio source
        if (audioSource != null && volumeSlider != null)
        {
            volumeSlider.value = audioSource.volume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    // Hàm được gọi khi giá trị của slider thay đổi
    void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
        }
    }
}
