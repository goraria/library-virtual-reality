// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Events;
//
// public class NoteController : MonoBehaviour {
//     [Header("Input")]
//     [SerializeField] private KeyCode closeKey;
//
//     //[Space(10)]
//     //[SerializeField] private PersonMovement player;
//
//     [Header("UI Text")]
//     [SerializeField] private GameObject noteCanvas;
//     // [SerializeField] private TMP_Text noteTextAreaUI;
//
//     //[Space(10)]
//     //[SerializeField]
//     //[TextArea] private string noteText;
//
//     [Space(10)]
//     [SerializeField]
//     private UnityEvent openEvent;
//
//     private bool isOpen = false;
//
//     public void ShowNote() {
//         // noteTextAreaUI.text = noteText;
//         noteCanvas.SetActive(true);
//         openEvent.Invoke();
//         // DisablePlayer(true);
//         isOpen = true;
//     }
//
//     void DisableNote() {
//         noteCanvas.SetActive(false);
//         // DisablePlayer(false);
//         isOpen = false;
//     }
//
//     private void Update() {
//         if (isOpen) {
//             if (Input.GetKeyDown(closeKey)) {
//                 DisableNote();
//             }
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NoteController : MonoBehaviour {
    [Header("Input")]
    [SerializeField] private KeyCode closeKey;
    [SerializeField] private KeyCode openImageKey = KeyCode.Return;  // Phím mở hình ảnh tiếp theo
    [SerializeField] private KeyCode previousImageKey = KeyCode.Backspace;  // Phím mở hình ảnh trước đó

    [Header("UI Elements")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private Image displayImage0;  // UI Image hiển thị hình ảnh
    [SerializeField] private Image displayImage1;  // UI Image hiển thị hình ảnh

    [Header("Image Array")]
    [SerializeField] private Sprite[] images0;  // Mảng chứa các hình ảnh để hiển thị
    [SerializeField] private Sprite[] images1;  // Mảng chứa các hình ảnh để hiển thị
    private int currentImageIndex = 0;  // Vị trí hình ảnh hiện tại trong mảng

    [Space(10)]
    [SerializeField] private UnityEvent openEvent;

    private bool isOpen = false;
    private bool isImageOpen = false;  // Trạng thái mở hình ảnh

    public void ShowNote() {
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        isOpen = true;
    }

    void DisableNote() {
        noteCanvas.SetActive(false);
        isOpen = false;
    }

    public void ShowImage() {
        if (images0.Length > 0 && images1.Length > 0) {
            // Hiển thị cùng một hình ảnh trên cả hai UI Image
            displayImage0.sprite = images0[currentImageIndex];
            displayImage1.sprite = images1[currentImageIndex];
            displayImage0.gameObject.SetActive(true);
            displayImage1.gameObject.SetActive(true);
            isImageOpen = true;
        }
    }

    public void HideImage() {
        displayImage0.gameObject.SetActive(false);
        displayImage1.gameObject.SetActive(false);
        isImageOpen = false;
    }

    private void Update() {
        // Đóng ghi chú nếu đang mở và nhấn phím đóng
        if (isOpen && Input.GetKeyDown(closeKey)) {
            DisableNote();
        }

        // Kiểm tra phím Enter để mở hình ảnh tiếp theo nếu chưa đến hình cuối
        if (Input.GetKeyDown(openImageKey)) {
            if (isImageOpen && currentImageIndex < images0.Length - 1 && currentImageIndex < images1.Length - 1) {
                currentImageIndex++;  // Chuyển sang hình ảnh tiếp theo
                ShowImage();
            } else if (!isImageOpen) {
                ShowImage();  // Mở hình ảnh nếu chưa mở
            }
        }

        // Kiểm tra phím Backspace để mở hình ảnh trước đó nếu chưa đến hình đầu
        if (Input.GetKeyDown(previousImageKey) && isImageOpen) {
            if (currentImageIndex > 0) {
                currentImageIndex--;  // Chuyển về hình ảnh trước đó
                ShowImage();
            }
        }

        // Đóng hình ảnh nếu đang mở và nhấn phím đóng
        if (isImageOpen && Input.GetKeyDown(closeKey)) {
            HideImage();
        }
    }
}

