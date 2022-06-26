using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * <summary>
 *  Only supports types that inherit IComparable
 * </summary>
 */
namespace spaghettoOS.Utils {
    public class CustomDict<TK, TV> : IEnumerable where TK : IComparable {
        private List<CustomDictEntry<TK, TV>> entries;
        public int Count => entries.Count;

        public CustomDict() {
            entries = new();
        }

        public void Add(TK key, TV value) {
            entries.Add(new(key, value));
        }

        public TV Get(TK key) {
            foreach (var entry in entries) {
                if (entry.key.CompareTo(key) == 0) return entry.value;
            }

            throw new NullReferenceException("CustomDict does not include key");
        }

        public void Set(TK key, TV value) {
            foreach (var entry in entries) {
                if (entry.key.CompareTo(key) == 0) {
                    entry.value = value;
                    return;
                }
            }

            entries.Add(new(key, value));
        }

        public bool Remove(TK key) {
            int i = 0;
            foreach (var entry in entries) {
                if (entry.key.CompareTo(key) == 0) {
                    entries.RemoveAt(i);
                    return true;
                }

                i++;
            }

            return false;
        }

        public void RemoveAt(int idx) {
            if (idx > entries.Count || idx < 0) throw new ArgumentOutOfRangeException("idx");
            entries.RemoveAt(idx);
        }

        public int IndexOf(TK key) {
            int i = 0;
            foreach (var entry in entries) {
                if (entry.key.CompareTo(key) == 0) {
                    entries.RemoveAt(i);
                    return i;
                }

                i++;
            }

            return -1;
        }

        public bool TryGet(TK key, out TV val) {
            foreach (var entry in entries) {
                if (entry.key.CompareTo(key) == 0) {
                    val = entry.value;
                    return true;
                }
            }

            val = default;
            return false;
        }

        public List<TV> AsValueList() {
            return (from entry in entries select entry.value).ToList();
        }

        public IEnumerator GetEnumerator() {
            return entries.GetEnumerator();
        }

        public TV this[TK key] {
            get => Get(key);
            set => Set(key, value);
        }
    }

    public class CustomDictEntry<TK, TV> where TK : IComparable {
        public TK key;
        public TV value;

        public CustomDictEntry(TK key, TV value) {
            this.key = key;
            this.value = value;
        }
    }
}
