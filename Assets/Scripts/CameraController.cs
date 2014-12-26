using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public int moveFrameWidth;
    public float speed;
    public float zoomSpeed;
    public Vector2 zoomFrame;
    public TileMap map;

    private float startCameraSize;

	void Start () {
        startCameraSize = camera.orthographicSize;
    }
	
	void Update () {
		Vector3 mouse = Input.mousePosition;
        Vector3 p = transform.localPosition;
        Vector3 position = new Vector3(p.x, p.y, p.z);

        int mapWidth = map.convertToArray().GetLength(0) + 1;
        int mapHeight = map.convertToArray().GetLength(1) + 1;

        float cameraHeight = camera.orthographicSize * 2.0f;
        float cameraWidth = cameraHeight * Screen.width / Screen.height;
        float cameraX = transform.localPosition.x - cameraWidth / 2;
        float cameraY = transform.localPosition.y - cameraHeight / 2;
        float zoom = Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed * camera.orthographicSize / startCameraSize * 10;

        float scaledSpeed = speed * Time.deltaTime * camera.orthographicSize / startCameraSize;

        bool right = mouse.x + moveFrameWidth > Screen.width || Input.GetKey(KeyCode.RightArrow);
        bool left = mouse.x - moveFrameWidth < 0 || Input.GetKey(KeyCode.LeftArrow);
        bool top = mouse.y + moveFrameWidth > Screen.height || Input.GetKey(KeyCode.UpArrow);
        bool bottom = mouse.y - moveFrameWidth < 0  || Input.GetKey(KeyCode.DownArrow);

        float scrollSpeed = 0;
        if (((right || left) && !top && !bottom) || ((bottom || top) && !right && !left)) {
            scrollSpeed = scaledSpeed;
        }
        if ((right || left) && (bottom || top)) {
            scrollSpeed = Mathf.Sin(Mathf.PI / 4) * scaledSpeed;
        }

        bool moveRight = right && cameraX + cameraWidth + scrollSpeed <= mapWidth;
        bool moveLeft = left && cameraX - scrollSpeed >= 0;
        bool moveTop = top && cameraY + cameraHeight + scrollSpeed <= mapHeight;
        bool moveBottom = bottom && cameraY - scrollSpeed >= 0;

        if (moveRight) {
            position.x += scrollSpeed;
        }
        else if (cameraX + cameraWidth <= mapWidth && right) {
            position.x = mapWidth - cameraWidth / 2;
        }
        if (moveLeft) {
            position.x -= scrollSpeed;
        }
        else if(cameraX > 0 && left) {
            position.x = cameraWidth / 2;
        }
        if (moveTop) {
            position.y += scrollSpeed;
        }
        else if (cameraY + cameraHeight <= mapHeight && top) {
            position.y = mapHeight - cameraHeight / 2;
        }
        if (moveBottom) {
            position.y -= scrollSpeed;
        }
        else if (cameraY > 0 && bottom) {
            position.y = cameraHeight / 2;
        }

        float newZoom = camera.orthographicSize;

        if (camera.orthographicSize - zoom < zoomFrame.x) {
            newZoom = zoomFrame.x;
        }
        else if (camera.orthographicSize - zoom > zoomFrame.y) {
            newZoom = zoomFrame.y;
        }
        else {
            newZoom -= zoom;
        }

        camera.orthographicSize = newZoom;

        if (cameraX + cameraWidth > mapWidth) {
            position.x = mapWidth - cameraWidth / 2;
        }
        if (cameraX < 0) {
            position.x = cameraWidth / 2;
        }
        if (cameraY + cameraHeight > mapHeight) {
            position.y = mapHeight - cameraHeight / 2;
        }
        if (cameraY < 0) {
            position.y = cameraHeight / 2;
        }

        transform.localPosition = position;
	}
}
