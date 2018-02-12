using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UidGenerator {

    private static uint m_uid = 0;
    //TODO This will eventually loop around max int.  Will need a smarter way to generate id's eventually.
	public static uint getNewUid()
    {
        return m_uid++;
    }
}
