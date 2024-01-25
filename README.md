# ACME-Datamill
The ACME software package consists of 
i) an ensemble of crop models,
ii) a database enabling the management of input and output data of these crop
models (‘MasterInputOutput’),
iii) a database containing tables that links the variables in the 
MasterInputOutput to the variables in the different crop models 
(‘ModelDictionnary’), and
iv) the software that generates the data flows between these databases and 
runs the crop models – named ‘DataMill’. 

These components are described in the next paragraphs. 

The crop model ensemble comprises a number of dynamic crop models that simulate
at daily time step key variables of the cropping system. Each crop model is an 
executable file containing mathematical equations translated into a computer 
program, along with the commands handling the inputs and outputs from and to a 
file system specific to each model. This file system is hereafter referred to 
as the ‘native files’ of the crop model. Besides, each crop model has its set 
of input parameters and variables, and its simulated output variables, hereafter
named ‘inputs and outputs’ of the model. Currently, ACME incorporates the three 
following crop models: Dssat (Jones et al., 2003), Stics (Brisson et al., 2003),
and Celsius (Ricome et al., 2017). Any other crop model compatible with 
standalone execution could be included in ACME 

MasterInputOutput is the database containing selected input variables and output
variables for running ACME. In its current state, this set of variables, 
named ACME inputs and ACME outputs, their names and units, was identified by 
a team of agronomists as the minimum set of variables required to evaluate the 
impact of climatic risks on cropping systems. It can be modified to include any
selection of variables from the set of input and outputs variables formed by the
union of all the sets of variables of each specific model. 
As a result, the total number of input (or output) variables of a specific model is greater or equal to the number of ACME inputs (or outputs). Certain input variables of a given model may match ACME input variables, or input variables of other models of the ensemble. Among them, some may share identical names and units, while others may differ in one or both of these aspects. Certain input variables may be model-specific, and while there may be conceptual similarities, mathematical transformation can link these input variables. ACME input variables are organized in tables corresponding to each key component of the simulated cropping system (e.g. soil; weather; crop; and management).
ModelDictionnary is the database containing: i) the list of ACME input and output variables with their description and type (e.g. real or integer number, text, Boolean), ii) the list of crop model inputs, specific to the model of the ensemble,  iii) the data defining the link between ACME inputs and model-specific inputs for each crop model of the ensemble, including the mathematical transformation applied to ensure the match between model-specific inputs and ACME inputs, whenever applicable, iv) default fixed values of model-specific inputs that are not related to any ACME input.
DataMill is the executable code written in Visual Basic (VB.net). It reads ACME input variables from the MasterInputOutput database. For each model-specific input, it assigns a value based on the link defined in the ModelDictionnary, i.e. either using the default value or applying the mathematical transformation. Then, each model-specific input is written in the native file system of the model, and DataMill launches the model simulations and distributes the computing tasks across the processor cores. Finally, the code reads the selected output variables from the native file system of each crop model and writes them into the MasterInputOuptut database. The main input table in the MasterInputOuptut database, containing the list of simulation units, is indexed with a unique identifier, used for naming the files in the native file system of the models. This ensures immediate and secure retrieval of simulation outputs and inputs for comparison and data analysis.
