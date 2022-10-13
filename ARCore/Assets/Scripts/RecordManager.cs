using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Google.XR.ARCoreExtensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Google.XR.ARCoreExtensions.Internal;
    using Unity.Collections;
    using UnityEngine;

#if UNITY_ANDROID
    using UnityEngine.XR.ARCore;
#endif // UNITY_ANDROID
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    public class RecordManager : MonoBehaviour
    {
        public ARSession session;
        public PlaybackResult playbackResult;
        public ARRecordingManager recordingManager;
        public ARPlaybackManager ARPlaybackManager;
        public bool setPlaybackDataset = false;
        public float timeout = 0f;

        private void Awake()
        {
            ARCoreRecordingConfig recordingConfig = ScriptableObject.CreateInstance<ARCoreRecordingConfig>();
            Uri datasetUri = new System.Uri(Application.persistentDataPath + "/" + "test.mp4");
            recordingConfig.Mp4DatasetUri = datasetUri;

            recordingManager.StartRecording(recordingConfig);
        }


        public void OnMenuStopClick()
        {
            recordingManager.StopRecording();
        }

        public void PlaybackDataset()
        {
            setPlaybackDataset = true;

            // Pause the current AR session.
            session.enabled = false;

            // Set a timeout for retrying playback retrieval.
            timeout = 10f;
        }

        private void Update()
        {
            session.enabled = false;

            Uri datasetUri = new System.Uri(Application.persistentDataPath + "/" + "test.mp4");
            ARPlaybackManager.SetPlaybackDatasetUri(datasetUri);

            session.enabled = true;

            if (setPlaybackDataset)
            {
                PlaybackResult result = ARPlaybackManager.SetPlaybackDatasetUri(datasetUri);
                if (result == PlaybackResult.ErrorPlaybackFailed || result == PlaybackResult.SessionNotReady)
                {
                    // Try to set the dataset again in the next frame.
                    timeout -= Time.deltaTime;
                }
                else
                {
                    // Do not set the timeout if the result is something other than ErrorPlaybackFailed.
                    timeout = -1f;
                }

                if (timeout < 0.0f)
                {
                    setPlaybackDataset = false;
                    // If playback is successful, proceed as usual.
                    // If playback is not successful, handle the error appropriately.
                }
            }


        }


    }

}
