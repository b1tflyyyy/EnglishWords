using System;

namespace EnglishWords.BL.Model
{
    [Serializable] public class Verb
    {
        #region Properties

        public int Id { get; }

        public string FirstForm { get; }

        public string SecondForm { get; }

        public string ThirdForm { get; }

        #endregion


        public Verb(int id, 
                    string firstForm, 
                    string secondForm, 
                    string thirdForm)
        {
            // TODO: Check input data
            #region Check input data

            if (id < 0) throw new ArgumentException(nameof(id), "id < 0");
            if(firstForm == null) throw new ArgumentNullException(nameof(firstForm));
            if (secondForm == null) throw new ArgumentNullException(nameof(secondForm));
            if(thirdForm == null) throw new ArgumentNullException(nameof(thirdForm));

            #endregion
            
            Id = id;
            FirstForm = firstForm;
            SecondForm = secondForm;
            ThirdForm = thirdForm;
        }
    }
}
