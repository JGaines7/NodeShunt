using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInfo : MonoBehaviour {

    public List<uint> m_attachedLines;
    public uint m_nodeUid = UidGenerator.getNewUid();

    bool attachLine(uint lineId)
    {
        bool rv = false;
        if(m_attachedLines.IndexOf(lineId) == -1)
        {
            m_attachedLines.Add(lineId);
            rv = true;
        }
        return rv;
    }

    bool detachLine(uint lineId)
    {
        bool rv = false;
        int index = m_attachedLines.IndexOf(lineId);
        if (index == -1)
        {
            m_attachedLines.RemoveAt(index);
            rv = true;
        }
        return rv;
    }


}
