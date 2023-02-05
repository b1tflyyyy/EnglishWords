using EnglishWords.BL.Model;
using System;

namespace EnglishWords.BL.Controller
{
    [Serializable] public class VerbController : ControllerBase
    {
        /// <summary>
        /// List of verbs in which there was an error.
        /// </summary>
        private List<Verb> _listErrorVerbs = new List<Verb>();

        /// <summary>
        /// List of all verbs.
        /// </summary>
        private List<Verb> _verbs = new List<Verb>();

        /// <summary>
        /// Amount of all verbs.
        /// </summary>
        private int count => _verbs.Count;

        /// <summary>
        /// Path for save/load data.
        /// </summary>
        private const string VERB_PATH = "Verbs.bin";

        /// <summary>
        /// Add a verb.
        /// </summary>
        /// <param name="verb"></param>
        public void AddVerb(Verb verb) => AddData(verb, _verbs, VERB_PATH);

        /// <summary>
        /// Get the verb by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Verb GetVerb(int id) => GetData(id, count, _verbs);

        // TODO: compare method here

        /// <summary>
        /// Get the incorrect verbs.
        /// </summary>
        /// <returns></returns>
        public List<Verb> GetIncorrectVerbs() => GetIncorrectAnswers(_listErrorVerbs);

        /// <summary>
        /// Is serializable data available? 
        /// </summary>
        /// <returns></returns>
        public bool CheckVerbAvailable() => CheckDataAvailable<Verb>(VERB_PATH);

        /// <summary>
        /// Load words.
        /// </summary>
        /// <returns></returns>
        public int LoadVerbs() { _verbs = LoadData<Verb>(VERB_PATH); return count; }
    }
}
