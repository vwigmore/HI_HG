/*
   Copyright 2015 Manus VR

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

     http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
 */

using UnityEngine;
using ManusMachina;

public class HandController : MonoBehaviour {
    public GLOVE_HAND hand;
    public Glove glove;

    void Start() {
        Manus.ManusInit();
        glove = new Glove(hand);
    }

    void Update() {
        // Add your gameplay code here.
    }

    void onApplicationQuit() {
        Manus.ManusExit();
    }
}