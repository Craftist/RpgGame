using System;
using System.Collections.Generic;

namespace RpgGameCore2.Screens
{
    public abstract class DialogueScreen : Screen
    {
        /// <summary>
        /// Dialogue parts.
        /// </summary>
        public abstract List<DialoguePart> Dialogues { get; }

        /// <summary>
        /// The screen that will be gone to after the dialogue ends.
        /// </summary>
        public abstract Func<Screen> NextScreen { get; set; }

        public int CurrentPartId;
        public List<(string line, bool isPlayerLine, bool isTechnical)> DialogueHistory = new List<(string line, bool isPlayerLine, bool isTechnical)>();
    }

    public class DialoguePart
    {
        public enum PartType
        {
            Actions, InputText
        }
        
        public Func<string> Line;
        public Func<bool> IsTechnical;
        public List<ScreenAction> Actions;
        
        /// <summary>
        /// Allows to jump to a particular ID of the dialogue. If omitted, the next part is implied.
        /// The out of bounds value results in the switching to the next screen (NextScreen property of the DialogueScreen).
        /// </summary>
        public Func<int> JumpTo;
        
        /// <summary>
        /// Type of the dialogue part.
        /// Action corresponds to standard screen, with a list of actions.
        /// InputText allows to enter arbitrary text.
        /// </summary>
        public Func<PartType> Type = () => PartType.Actions;
        
        /// <summary>
        /// The action that is performed on the input. Fires only if Type == PartType.InputText.
        /// Parameter corresponds to what was written. If nothing was written, the parameter is equal to null.
        /// </summary>
        public Action<string> ActionOnInput;

        public bool IsPlayerLine;
    }
}

